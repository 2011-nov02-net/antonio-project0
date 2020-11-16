drop table if exists orderline;
drop table if exists orders;
drop table if exists inventory;
drop table if exists book;
drop table if exists customer;
drop table if exists location;

create table location(
	id int primary key identity,
	name nvarchar(50) not null
)

create table customer(
	id int primary key identity,
	first_name nvarchar(50) not null,
	last_name nvarchar(50) not null,
	location_id int foreign key  references location(id)
)

ALTER TABLE customer
ADD CONSTRAINT df_location
DEFAULT 1 FOR location_id;

create table book(
	isbn nvarchar(15) primary key,
	name nvarchar(50) not null,
	price decimal(19,4) not null,
	author_first_name nvarchar(50),
	author_last_name nvarchar(50)
)

create table inventory(
	location_id int foreign key references location(id),
	book_isbn nvarchar(15) foreign key references book(isbn),
	quantity int,
	primary key (location_id, book_isbn)
)

create table orders(
	id int primary key identity,
	customer_id int not null foreign key references customer(id),
	location_id int not null foreign key references location(id),
	order_date datetime default current_timestamp
)

create table orderline(
	id int primary key identity,
	order_id int not null foreign key references orders(id),
	book_isbn nvarchar(15) not null foreign key references book(isbn),
	quantity int not null
)

insert into location (name) values ('Reston, VA');
insert into location (name) values ('Dallas, TX');
insert into location (name) values ('Tampa, FL');
insert into location (name) values ('New York, NY');
insert into location (name) values ('Orlando, FL');
insert into location (name) values ('Morgantown, WV');

insert into customer (first_name, last_name) values ('Antonio','Mendez');
insert into customer (first_name, last_name) values ('Darko','Mendez');
insert into customer (first_name, last_name) values ('Gavin','Mendez');
insert into customer (first_name, last_name) values ('Kayla','Mendez'); 


insert into book (isbn, name, price, author_first_name, author_last_name) values ('978-0525948926', 'Atlas Shrugged', 29.99, 'Ayn', 'Rand');
insert into book (isbn, name, price, author_first_name, author_last_name) values ('978-0452286757', 'The Fountainhead', 27.99, 'Ayn', 'Rand');
insert into book (isbn, name, price, author_first_name, author_last_name) values ('978-1640320437', 'Anthem', 9.99, 'Ayn', 'Rand');
insert into book (isbn, name, price, author_first_name, author_last_name) values ('978-0553103540', 'A Game of Thrones (Song of Ice and Fire)', 26.53, 'George R.R.', 'Martin');
insert into book (isbn, name, price, author_first_name, author_last_name) values ('978-1617294563', 'Entity Framework Core in Action', 45.28, 'Jon P', 'Smith');

-- Reston Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0525948926',0)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0452286757',0)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-1640320437',15)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-0553103540',3)
insert into inventory (location_id, book_isbn, quantity) values (1, '978-1617294563',999)

-- Dallas Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0525948926', 873)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0452286757', 0)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-1640320437', 48)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-0553103540', 16)
insert into inventory (location_id, book_isbn, quantity) values (2, '978-1617294563', 999)

-- Tampa Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0452286757', 11)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-1640320437', 65)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-0553103540', 100)
insert into inventory (location_id, book_isbn, quantity) values (3, '978-1617294563', 0)

-- New York Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0525948926', 95)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0452286757', 22)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-1640320437', 123)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-0553103540', 999)
insert into inventory (location_id, book_isbn, quantity) values (4, '978-1617294563', 10)

-- Orlando Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0452286757', 22)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-1640320437', 136)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-0553103540', 41)
insert into inventory (location_id, book_isbn, quantity) values (5, '978-1617294563', 52)

-- Morgantown Initial Inventory
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0525948926', 78)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0452286757', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-1640320437', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-0553103540', 0)
insert into inventory (location_id, book_isbn, quantity) values (6, '978-1617294563', 665)

insert into orders (customer_id, location_id) values (1,1);
insert into orders (customer_id, location_id) values (2,1);

insert into orderline(order_id, book_isbn, quantity) values (1, '978-0525948926',1)
insert into orderline(order_id, book_isbn, quantity) values (1, '978-0452286757',1)
insert into orderline(order_id, book_isbn, quantity) values (1, '978-1640320437',1)


insert into orderline(order_id, book_isbn, quantity) values (2, '978-0553103540',1)
insert into orderline(order_id, book_isbn, quantity) values (2, '978-1617294563',23)

select * from customer, location where customer.id = 3 AND customer.location_id = location.id;

insert into orders (customer_id, location_id) values (4,5);
insert into orderline (order_id, book_isbn, quantity) values (3, '978-1617294563', 8)
update inventory set quantity = 655 where location_id = 5 AND book_isbn = '978-1617294563'