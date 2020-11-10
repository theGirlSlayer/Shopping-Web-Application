CREATE TABLE tAdmin
(
    username VARCHAR(50) PRIMARY KEY,
    password VARCHAR(50) NOT NULL
);
CREATE TABLE tCatergory
(
    id int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) not NULL
);
CREATE TABLE tTag
(
    id int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) not NULL
);
CREATE TABLE tProducter
(
    id int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) not null,
    avatar VARCHAR(200),
    note TEXT
);
CREATE TABLE tUser
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    firstName VARCHAR(50) not NULL,
    lastName VARCHAR(50) not null,
    displayName VARCHAR(50) NOT NULL,
    phoneNumber int UNSIGNED not null,
    email Varchar(50) Not Null,
    facebook VARCHAR(100),
    twiter VARCHAR(100),
    instagram VARCHAR(100),
    address VARCHAR(100),
    town VARCHAR(100),
    companyName VARCHAR(100),
    zipCode int UNSIGNED,
    username VARCHAR(50) UNIQUE NOT NULL,
    password Varchar(128) Not null
);
CREATE TABLE tBookedTranstedDate
(
    idBill int UNSIGNED not NULL,
    billStatus TINYINT NOt NULL default 0,
    idSendUser int UNSIGNED NOT NULL,
    idRecevieUser int UNSIGNED NOT NULL,
    transportFee int UNSIGNED NOT NULL DEFAULT 0,
    bookedDateTime DATETIME not NULL DEFAULT NOw(),
    transtedDateTime DATETIME,
    primary key (idBill, idSendUser, idRecevieUser),
    FOREIGN KEY (idSendUser) References tUser(id),
    FOREIGN KEY (idRecevieUser) References tUser(id)
);
CREATE TABLE tProduct
(
    id int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) not null,
    idUser int UNSIGNED NOT NULL,
    idProducter int UNSIGNED not null,
    remainderQuantity int UNSIGNED not null DEFAULT 0,
    avatar VARCHAR(200) NOT NULL,
    note TEXT,
    isSale bit not null default 0,
    shortNote VARCHAR(200) not null,
    price int UNSIGNED not null,
    oldPrice int UNSIGNED not null,
    FOREIGN KEY (idUser) References tUser(id),
    FOREIGN KEY (idProducter) References tProducter(id)
);
create TABLE tDealerRefProduct
(
    idUser int UNSIGNED not null,
    idProduct int UNSIGNED NOT NULL,
    primary key (idUser, idProduct),
    FOREIGN KEY (idUser) References tUser(id),
    FOREIGN KEY (idProduct) References tProduct(id)
);
CREATE TABLE tProductRefCatergory
(
    idCatergory int UNSIGNED,
    idProduct int UNSIGNED,
    primary key (idCatergory, idProduct),
    FOREIGN KEY (idCatergory) References tCatergory(id),
    FOREIGN KEY (idProduct) References tProduct(id)
);
CREATE TABLE tProductRefTag
(
    idTag int UNSIGNED,
    idProduct int UNSIGNED,
    primary key (idTag,idProduct),
    FOREIGN KEY (idTag) References tTag(id),
    FOREIGN KEY (idProduct) References tProduct(id)
);
CREATE TABLE tProductImage
(
    idProduct int UNSIGNED,
    filePath VARCHAR(200),
    primary key (idProduct,filePath),
    FOREIGN KEY (idProduct) References tProduct(id)
);
CREATE TABLE tFavorateProduct
(
    idUser int UNSIGNED not NULL,
    idProduct int UNSIGNED not NULL,
    primary key (idUser,idProduct),
    FOREIGN KEY (idProduct) References tProduct(id),
    FOREIGN KEY (idUser) References tUser(id)
);
CREATE TABLE tOverview
(
    postDate DATETIME PRIMARY KEY default now(),
    idUser int UNSIGNED not null,
    idProduct int UNSIGNED not null,
    overviewText text not null,
    FOREIGN KEY (idProduct) References tProduct(id),
    FOREIGN KEY (idUser) References tUser(id)
);
CREATE TABLE tBill
(
    id int UNSIGNED,
    idProduct int UNSIGNED NOT NULL,
    quantity int UNSIGNED not null,
    priceAtTime int UNSIGNED NOT NULL,
    primary key (id,idProduct),
    FOREIGN KEY (idProduct) References tProduct(id)
);
create TABLE tCountry
(
    id TINYINT UNSIGNED PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);
CREATE TABLE tZipCode
(
    id TINYINT UNSIGNED PRIMARY KEY,
    name VARCHAR(100)
);

alter TABLE tProduct MODIFY COLUMN isSale TINYINT(1) not null DEFAULT 0
insert tProduct (name, idUser, idProducter, remainderQuantity, avatar, note, isSale, shortNote, price, oldPrice) values ('Giày ba sọc', 2,1,20,'man-2.jpg', 'Phù hợp cho dân 3 que', 1, 'Như cặc', 2000, 5000);


ALTER TABLE tUser add COLUMN zipCode int unsigned

SELECT * from tFavorateProduct WHERE idUser = 1
insert tFavorateProduct VALUE(1,13)