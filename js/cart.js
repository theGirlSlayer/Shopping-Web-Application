var itemsListContainer = document.getElementById('items-in-cart')
var addToCartButton = document.getElementsByClassName("w-icon active")
for (let i = 0; i < addToCartButton.length; i++) {
    const element = addToCartButton[i];
    element.addEventListener('click',() => {
      var productID = element.parentElement.parentElement.parentElement.parentElement.id
      var picture = element.parentElement.parentElement.children[0].children[0].src
      var productPrice = element.parentElement.parentElement.parentElement.getElementsByClassName('product-price')[0].textContent.split('\n')[0]
      var itemName = element.parentElement.parentElement.parentElement.getElementsByClassName("item-name")[0].textContent
      AddToCart(productID,picture,productPrice,itemName)
    })
}
var removeButton = document.getElementsByClassName("si-close")
for (let i = 0; i < removeButton.length; i++) {
  const element = removeButton[i];
  element.addEventListener('click', () =>
  {
    Remove(element.parentElement.id)
  })
}
var addToCartInPage = document.getElementsByClassName("primary-btn pd-cart")[0]
addToCartInPage.addEventListener('click', ()=>{
  var productID = document.URL.split("=")[1]
  var picture = document.getElementsByClassName("zoomImg")[0].src
  var productPrice = document.getElementsByClassName("item-price")[0].id
  var itemName = document.getElementById("item-name").innerHTML
  AddToCart(productID,picture,productPrice,itemName)
})
function UpdateTotalPrice() {
  var priceInfos = document.getElementsByClassName("priceInfo")
  var TotalPrice = 0
  var count =0
  for (var i = 0; i < priceInfos.length; i++) {
      const element = priceInfos[i];
      var Para = element.innerHTML.replace(' ', '').split('x')
      TotalPrice += parseInt(Para[0]) * parseInt(Para[1])
      count += parseInt(Para[1])
  }
  document.getElementById("total-price").innerHTML = TotalPrice
  document.getElementById("item-count").innerHTML = count
  document.getElementsByClassName("cart-price")[0].innerHTML = TotalPrice
}
function Remove(ID)
{
  $.ajax({
    url : "/Product/RemoveProductInCart",
    type: "GET",
    dataType: "JSON",
    data:{ id : ID}
  })
    var itemElement = document.getElementById(ID)
    var quantityElement = itemElement.getElementsByClassName('priceInfo')[0]
    var quantity = parseInt(quantityElement.innerHTML.replace(' ', '').split('x')[1])
    if (quantity == 1) {
      itemElement.remove()
    }
    else
    {
      quantityElement.innerHTML = quantityElement.innerHTML.replace(' ', '').split('x')[0] + " x " + (quantity - 1)
    }
    UpdateTotalPrice()
}
function AddToCart(productID,picture,productPrice,itemName)
{

  $.ajax({
    url : "/Product/AddToCart",
    type: "GET",
    dataType: "JSON",
    data:{ id : productID}
  })
  if (itemsListContainer.childElementCount == 0) {
    var itemElement = document.createElement('tr')
      itemElement.id =  productID
      itemElement.innerHTML = '<td class="si-pic"><img src="' + picture + '" alt=""></td><td class="si-text"><div class="product-selected"><p class="priceInfo">'+productPrice+' x 1</p><h6>'+itemName+'</h6></div></td><td class="si-close"><i class="ti-close"></i></td>'
      itemsListContainer.appendChild(itemElement)
      UpdateTotalPrice()
      var removeButton = itemElement.getElementsByClassName("si-close")[0]
      removeButton.addEventListener('click', () =>
      {
        Remove(productID)
      })
      return
  }
  for (let index = 0; index < itemsListContainer.childElementCount; index++) {
    const item = itemsListContainer.children[index];
    if (productID == item.id) {
      var quantityElement = item.getElementsByClassName('priceInfo')[0]
      var quantity = parseInt(quantityElement.innerHTML.replace(' ', '').split('x')[1])
      quantityElement.innerHTML = quantityElement.innerHTML.replace(' ', '').split('x')[0] + " x " + (quantity + 1)
      UpdateTotalPrice()
      return
    }
  }
      var itemElement = document.createElement('tr')
      itemElement.id = productID
      itemElement.innerHTML = '<td class="si-pic"><img src="' + picture + '" alt=""></td><td class="si-text"><div class="product-selected"><p class="priceInfo">'+productPrice+' x 1</p><h6>'+itemName+'</h6></div></td><td class="si-close"><i class="ti-close"></i></td>'
      itemsListContainer.appendChild(itemElement)
      var removeButton = document.getElementById(productID).getElementsByClassName("si-close")[0]
      var removeButton = itemElement.getElementsByClassName("si-close")[0]
      removeButton.addEventListener('click', () =>
      {
          Remove(productID)
      })
      UpdateTotalPrice()
}