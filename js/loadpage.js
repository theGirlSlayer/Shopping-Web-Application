var pageIndex = 21;
var loadbutton = document.getElementById("load-product");

loadbutton.addEventListener('click', ()=>
{
    $.ajax({
        url: "/Product/GetProductByPageIndex",
        type: "GET",
        dataType: "JSON",
        data:{index : pageIndex},
        success: function(productList)
        {
            var products = document.getElementById('row-item');
            for (let i = 0; i < productList.length; i++) {
                const element = productList[i];
                var product = document.createElement('div')
                product.id = element.id
                product.className = 'col-lg-4 col-sm-6'
                product.innerHTML += '<div class="product-item"><div class="pi-pic"><img class="picture-item" src="/img/products/'+element.avatar+'" alt="">'
                if (element.IsSale)
                    {
                        product.innerHTML += '<div class="sale pp-sale">Sale</div>'
                    }
                product.innerHTML += '<div class="icon"><i class="icon_heart_alt"></i></div><ul><li class="w-icon active"><a class="icon_bag_alts"><i class="icon_bag_alt"></i></a></li><li class="quick-view"><a href="/Product/ProductPage?id='+element.id+'">View</a></li><li class="w-icon"><a href="#"><i class="fa fa-random"></i></a></li></ul></div><div class="pi-text"><div class="catagory-name">Remainder : '+ element.remainderQuantity+'</div><a href="/Product/ProductPage?id='+element.id+'"><h5 class="item-name">'+element.name+'</h5></a><div class="product-price">'+ element.price +'<span>$'+element.oldPrice+'</span></div></div></div>'
                products.appendChild(product)
            }
            pageIndex +=21;
        }
    })
})
