drop schema if exists travel_agency;
create schema travel_agency default character set utf8 default collate utf8_unicode_ci;
use travel_agency;

create table person
(
	JMB char(13),
    FirstName varchar(255) not null,
    LastName varchar(255) not null,
    Address varchar(255) not null,
    DateOfBirth date not null,
    Email varchar(255) not null,
    PhoneNumber varchar(20) not null,
    primary key(JMB)
    );

insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('1208983126116', 'Ana', 'Stević', 'Ulica 3', '1983-08-03', 'anastevic@gmail.com', '+387661136255');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('2202550126416', 'Marija', 'Đurić', 'Ulica 10', '2000-01-22', 'marijadjuric@gmail.com','+38765420605');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('1210987255018', 'Nemanja', 'Komljenović', 'Ulica 26', '1987-10-12', 'nemanjakomljenovic@gmail.com','+38766779128');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('0804001123336', 'Igor', 'Dojčinović', 'Ulica 1', '2001-04-08', 'igordojcinovic@gmail.com','+38766317952');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('1412978126987', 'Ivana', 'Kos', 'Ulica 11', '1978-12-14', 'ivanakos@gmail.com','+38765196255');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('2255198112356', 'Janko', 'Ristić', 'Ulica 12', '1981-01-25', 'jankoristic@gmail.com', '+38766008808');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('2802001123447', 'Andrej', 'Knežević', 'Ulica 74', '2001-02-28', 'andrejknezevic@gmail.com','+38765491628');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('0404992126546', 'Nevena', 'Kovač', 'Ulica 84', '1992-04-04', 'nevenakovac@gmail.com', '+38765476328');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('1407964123117', 'Mihajlo', 'Kos', 'Ulica 9', '1964-07-14', 'mihajlokos@gmail.com','+38765494448');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('1806994126336', 'Jovana', 'Irić', 'Ulica 115', '1994-06-18', 'jovanairic@gmail.com', '+38765411128');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('5978428126987', 'Marijan', 'Irić', 'Ulica 1', '1994-06-18', 'marijaniric@gmail.com', '+38765466628');
insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values ('6598744126336', 'Nikolaj', 'Irić', 'Ulica 15', '1994-06-18', 'nikolajiric@gmail.com', '+38765369628');


create table customer
(
	JMB char(13),
    primary key (JMB),
    constraint FK_customer_person
    foreign key (JMB)
    references person (JMB)
    on update cascade on delete restrict
);    

insert into customer (JMB) values ('1208983126116');
insert into customer (JMB) values ('2202550126416');
insert into customer (JMB) values ('1210987255018');
insert into customer (JMB) values ('0804001123336');
insert into customer (JMB) values ('1407964123117');

create table employee
(
	JMB char(13),
    primary key (JMB),
    Salary decimal(15,2) not null,
    EmploymentDate date not null,
    Username varchar(255) not null UNIQUE,
    PasswordString varchar(255) not null,
    PreferredTheme VARCHAR(20) DEFAULT 'default',
	RoleType ENUM('admin', 'salesAgent') not null,
    constraint FK_employee_person
    foreign key (JMB)
    references person (JMB)
    on update cascade on delete restrict
); 

insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values ('1412978126987', 1800.00, 'admin1', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=', '2005-10-01', 'admin');
insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values ('5978428126987', 2000.00, 'korisnickoIme2', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=','2010-02-01', 'salesAgent');
insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values ('0404992126546', 1790.00, 'korisnickoIme3', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=','2019-04-06', 'salesAgent');
insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values ('6598744126336', 1625.00, 'korisnickoIme4', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=','2021-11-18', 'salesAgent');
insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values ('1806994126336', 1625.00, 'korisnickoIme5', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=','2018-07-18', 'salesAgent');



create table country
(
    CountryName varchar(255),
    primary key(CountryName)
);

insert into country (CountryName) values ('Italija');
insert into country (CountryName) values ('Grčka');
insert into country (CountryName) values ('Francuska');
insert into country (CountryName) values ('Engleska');
insert into country (CountryName) values ('Srbija');
insert into country (CountryName) values ('Bosna i Hercegovina');
insert into country (CountryName) values ('Hrvatska');
insert into country (CountryName) values ('Portugal');

create table destination
(
	PostCode int(11),
    DestinationName varchar(255),
    primary key(PostCode, DestinationName),
    About varchar(2000) not null,
    Distance int not null,
    LocalLanguage varchar(255) not null,
    CountryName varchar(255) not null,
    constraint FK_destination_country
    foreign key (CountryName)
    references country (CountryName)
    on update cascade on delete restrict
);
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (20123, 'Milano', 'Prošetajte ulicama kolijevke mode i dizajna.', 814, 'italijanski', 'Italija');
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (11742, 'Atina', 'Upoznajte čari Antičke Grčke i istražite mediteransku kuhinju.', 1422, 'grčki', 'Grčka');
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (70123, 'Pariz', 'Posjetite čuvenu "Mona Lizu" ili prošetajte ispred Ajfelovog tornja', 1576, 'francuski', 'Francuska');
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (37122, 'Verona', 'Posjetite mjesto vječne ljubavi Romea i Julije.', 664, 'italijanski', 'Italija');
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (10000, 'Zagreb', 'Jedini je glavni grad u svijetu koji počinje na slovo Z.Također, jedini je evropski glavni grad sa skijalištem.', 1255, 'hrvatski', 'Hrvatska');
insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values (1900, 'Lisabon', 'Lisabon je jedan od najstarijih gradova na svijetu. U gradu se nalaze dva mjesta uvrštena na pAbout Svjetske baštine UNESCO-a: Belémski toranj i Jeronimitski samostan. ', 2.935, 'portugalski', 'Portugal');


create table hotel
(
	HotelId int auto_increment,
    primary key (HotelId),
    Name_of varchar(255) not null,
    RoomCount int not null,
    Address varchar(255) not null,
    Email varchar(255) not null,
    ContainsRestaurant tinyint not null,
    DestinationName varchar(255),
    PostCode int(11) not null,
    constraint FK_hotel_destination
    foreign key (PostCode, DestinationName)
    references destination (PostCode, DestinationName)
    on update cascade on delete restrict
);

insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Spadari Al Duomo', 2, 'Via Spadari 11', 'reservation@spadarihotel.com', 1, 20123, 'Milano');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Hotel Astoria', 2, '42 rue de Moscou 8th Arr.', 'reservation@astoriahotel.com', 1, 70123, 'Pariz');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('The Atina Hotel', 3, 'Sygrou Avenue 10', 'reservation@gatehotel.com', 0, 11742, 'Atina');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Hotel Indigo Verona', 2, 'Corso Porta Nuova 105', 'reservation@indigohotel.com', 0, 37122, 'Verona');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Hotel Mundial', 6, 'Praça Martim Moniz 2 Santa Maria Maior', 'reservation@mundialhotel.com', 1, 1900, 'Lisabon');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Esplanade Zagreb Hotel', 4, 'Mihanovićeva 1', 'reservation@esplanadehotel.com', 1, 10000, 'Zagreb');
insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values ('Za brisanje', 4, 'Mihanovićeva 1', 'reservation@esplanadehotel.com', 1, 10000, 'Zagreb');


create table package
(
	PackageId int auto_increment,
    primary key(PackageId),
    StartDate date not null,
    EndDate date not null, 
    Price decimal(15,2) not null,
    About varchar(2000),
    DestinationName varchar(255),
    PostCode int(11) not null,
    constraint FK_package_destination
    foreign key (PostCode, DestinationName)
    references destination (PostCode, DestinationName)
    on update cascade on delete restrict
);

insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values ('2024-09-03', '2024-09-10', 1985.00, 'Sedam dana u Milanu.', 20123, 'Milano');
insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values ('2024-10-12', '2024-10-15', 814.00, 'Tri dana u Parizu.', 70123, 'Pariz');
insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values ('2024-12-31', '2025-01-02', 1600.00, 'Nova Godina u Atini.', 11742, 'Atina');
insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values ('2024-12-31', '2025-01-02', 2800.00, 'Nova Godina u Lisabonu.', 1900, 'Lisabon');
insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values ('2024-04-01', '2024-04-05', 1255.00, 'Četiri dana u Zagrebu.', 10000, 'Zagreb');


create table package_offers_hotel
(
	PackageId int,
    HotelId int,
    primary key(HotelId, PackageId),
    constraint FK_package_offers_hotel_hotel
    foreign key (HotelId)
    references hotel (HotelId)
    on update cascade on delete restrict,
    constraint FK_package_offers_hotel_package
    foreign key (PackageId)
    references package (PackageId)
    on update cascade on delete restrict
);

insert into package_offers_hotel (PackageId, HotelId) values (1, 1);
insert into package_offers_hotel (PackageId, HotelId) values (3, 3);
insert into package_offers_hotel (PackageId, HotelId) values (2, 2);
insert into package_offers_hotel (PackageId, HotelId) values (4, 5);
insert into package_offers_hotel (PackageId, HotelId) values (5, 6);

create table reservation
(
	ReservationId int auto_increment,
    primary key (ReservationId),
    Price decimal(15,2) not null,
    PackageId int not null,
    HotelId int not null,
    CustomerJMB char(13) not null,
    EmployeeJMB char(13) not null,
    IsPayed tinyint not null,
    constraint FK_Reservation_hotel
    foreign key (HotelId)
    references hotel (HotelId)
    on update cascade on delete restrict,
    constraint FK_Reservation_package
    foreign key (PackageId)
    references package (PackageId)
    on update cascade on delete restrict,
     constraint FK_Reservation_customer
    foreign key (CustomerJMB)
    references customer (JMB)
    on update cascade on delete restrict,
     constraint FK_Reservation_sale_agent
    foreign key (EmployeeJMB)
    references employee (JMB)
    on update cascade on delete restrict
);

insert into Reservation (Price, IsPayed, PackageId, HotelId, CustomerJMB, EmployeeJMB) values (1985.00, false, 1, 1, '1208983126116', '1412978126987');
insert into Reservation (Price, IsPayed, PackageId, HotelId, CustomerJMB, EmployeeJMB) values (1985.00, false, 1, 1, '1210987255018', '1412978126987');
insert into Reservation (Price, IsPayed, PackageId, HotelId, CustomerJMB, EmployeeJMB) values (1985.00, false, 1, 1, '0804001123336', '1412978126987');


create table package_payment
(
	PaymentId int AUTO_INCREMENT,
    primary key(PaymentId),
    PaymentDate date not null,
    Amount decimal(15,2) not null,
    ReservationId int not null,
    constraint FK_package_payment_reservation
    foreign key(ReservationId)
    references Reservation (ReservationId)
    on update cascade on delete restrict
);

insert into package_payment (PaymentDate, Amount, ReservationId) values ('2023-09-01', 200.00, 1);
insert into package_payment (PaymentDate, Amount, ReservationId) values ('2023-09-02', 1255.00, 2);

create table hotel_payment
(
	PaymentId int AUTO_INCREMENT,
    primary key(PaymentId),
    PaymentDate date not null,
    Amount decimal(15,2) not null,
    ReservationId int not null,
    constraint FK_hotel_payment_reservation
    foreign key(ReservationId)
    references Reservation (ReservationId)
    on update cascade on delete restrict
);

insert into hotel_payment (PaymentDate, Amount, ReservationId) values ('2023-09-01', 400.00, 1);
insert into hotel_payment (PaymentDate, Amount, ReservationId) values ('2023-09-02', 280.00, 2);


create view customer_info (JMB, CustomerFirstName, Address, DateOfBirth, Email, PhoneNumber) as  
select p.JMB, concat(LastName, ', ', FirstName), Address, DateOfBirth, Email, PhoneNumber
from customer p inner join person o on o.JMB=p.JMB;

create view package_info (PackageId, Destination, Price, StartDate, EndDate, AboutPackage) as  
select a.PackageId, d.DestinationName, round(Price,2), StartDate, EndDate, a.About
from package a
inner join destination d on a.PostCode=d.PostCode and a.DestinationName = d.DestinationName;

create view reservation_info (ReservationId, PackageId, Destination, Hotel, CustomerFirstName, CustomerPhoneNumber, Price, IsPayed, SalesAgentName) as  
select ReservationId, a.PackageId, d.DestinationName, h.Name_of, concat(o.LastName, ', ',o.FirstName), o.PhoneNumber, round(r.Price,2), if(IsPayed, 'Yes', 'No') as Payed, CONCAT(s.LastName, ', ', s.FirstName) AS SalesAgentName
from Reservation r
inner join package a on a.PackageId = r.PackageId
inner join destination d on a.PostCode=d.PostCode
inner join hotel h on h.HotelId = r.HotelId
inner join (customer p inner join person o on p.JMB=o.JMB) on p.JMB = r.CustomerJMB
inner join (employee sa inner join person s on sa.JMB=s.JMB) on sa.JMB =  r.EmployeeJMB;
/*left outer join employee sa on sa.JMB = r.EmployeeJMB; */

create view reservation_payment_info (CustomerFirstName, JMB, PhoneNumber, Amount, PaymentDate, PaymentId, ReservationId, PayedToAgency, PayedToHotel) as  
select concat(FirstName, ' ', LastName), p.JMB, PhoneNumber, round(u.Amount,2), u.PaymentDate, u.PaymentId, r.ReservationId, round(u.Amount*0.2,2), round(u.Amount*0.8,2)
from customer p inner join person o on o.JMB=p.JMB
inner join Reservation r on r.CustomerJMB = p.JMB
inner join package_payment u on u.ReservationId = r.ReservationId;


create view total_payment_for_reservation (CustomerName, ReservationId, FinalPrice, Payed, Debt, IsPayed) as  
select concat(FirstName, ' ', LastName), r.ReservationId, round(r.Price,2), round(sum(u.Amount),2), if(r.Price-sum(u.Amount)<r.Price, round(r.Price-sum(u.Amount),2), r.Price), if(r.IsPayed, 'Jeste', 'Nije') as Payed
from customer p inner join person o on o.JMB=p.JMB
inner join Reservation r on r.CustomerJMB = p.JMB
left outer join package_payment u on u.ReservationId = r.ReservationId
group by r.ReservationId;


delimiter $$
create procedure pay_reservation(in uAmount int, in uReservationId int, out successfulPayment int, out message varchar(255))
begin

  declare PackageId int;
  declare amountPayed decimal;
  declare aPrice decimal;
  declare rIsPayed tinyint;
  
  select PackageId into PackageId
  from Reservation
  where ReservationId = uReservationId;
  
  select IsPayed into rIsPayed
  from Reservation
  where ReservationId = uReservationId;
  
  select Price into aPrice
  from Reservation
  where ReservationId = uReservationId;
  
  select COALESCE(SUM(Amount), 0) into amountPayed
  from package_payment
  where ReservationId = uReservationId;
  
  
  if rIsPayed then
    set message="Full amount has been payed. No need for new payment.";
    set successfulPayment=0;
 elseif (amountPayed+uAmount)>aPrice then
    set successfulPayment=0;
    set message="Amount is too big. Please enter smaller amount.";
  else
    insert into package_payment (PaymentDate, Amount, ReservationId) values (current_date(), uAmount, uReservationId);
    insert into hotel_payment (PaymentDate, Amount, ReservationId) values (current_date(), uAmount*0.8, uReservationId);
    if (amountPayed+uAmount) = aPrice then
		update Reservation
		set IsPayed=1
		where ReservationId = uReservationId;
    end if;
     set message="Payment successful.";
    set successfulPayment=1;
    end if;
    
  
end$$
delimiter ; 



delimiter $$
create procedure add_customer(in pJMB char(13), in pFirstName varchar(255), in pLastName varchar(255),
in pAddress varchar(255), in pDateOfBirth varchar(20), in pEmail varchar(255), in pphone varchar(20), out successful bool, out message varchar(255))
begin

    insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values (pJMB, pFirstName, pLastName, pAddress,str_to_date(pDateOfBirth, '%Y-%m-%d'), pEmail, pphone);
    insert into customer (JMB) values (pJMB);
   
    set successful=true;
    set message="customer added successfully.";
end$$
delimiter ;

delimiter $$
create procedure delete_customer(in pJMB char(13), out successful int, out message varchar(255))
begin

	DECLARE jmbReservation char(13);
	DECLARE customernum INT;
    DECLARE reservationNum INT;
    DECLARE pNum INT;
    DECLARE resId INT;
    
    SELECT COUNT(*) INTO customernum FROM customer WHERE JMB = pJMB;
    SELECT COUNT(*) INTO reservationNum FROM Reservation WHERE CustomerJMB = pJMB;
    SELECT COUNT(*) INTO pNum FROM customer WHERE JMB = pJMB;
    
    IF customernum>0 THEN
		IF reservationNum=0 THEN 
			

		DELETE FROM customer WHERE JMB = pJMB;
		DELETE FROM person WHERE JMB = pJMB;
        set successful=1;
        set message="customer deleted successfully.";
         ELSE
        set successful=0;
        set message="customer can't be deleted.";
    END IF;
    ELSE
        set successful=0;
        set message="customer can't be deleted.";
    END IF;

end$$
delimiter ;

delimiter $$
create procedure add_sales_agent(in pJMB char(13), in pFirstName varchar(255), in pLastName varchar(255),
in pAddress varchar(255), in pDateOfBirth varchar(20), in pEmail varchar(255), in pphone varchar(20), in pUsername varchar(255), in pPasswordString varchar(255),
in pEmploymentDate varchar(20), in pSalary decimal(15,2), out successful bool, out message varchar(255))
begin

    insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values (pJMB, pFirstName, pLastName, pAddress,str_to_date(pDateOfBirth, '%Y-%m-%d'), pEmail, pphone);
    insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values (pJMB, pSalary, pUsername, pPasswordString, str_to_date(pEmploymentDate, '%Y-%m-%d'),'salesAgent');
    set successful=true;
    set message="Sales agent added successfully.";
end$$
delimiter ;

delimiter $$
create procedure delete_employee(in pJMB char(13), out successful int, out message varchar(255))
begin

	DECLARE jmbReservation char(13);
	DECLARE employeenum INT;
    DECLARE reservationNum INT;
    DECLARE pNum INT;
    DECLARE resId INT;
    
    SELECT COUNT(*) INTO employeenum FROM employee WHERE JMB = pJMB;
    SELECT COUNT(*) INTO reservationNum FROM Reservation WHERE EmployeeJMB = pJMB;
    SELECT COUNT(*) INTO pNum FROM employee WHERE JMB = pJMB;
    
    IF employeenum>0 THEN
		IF reservationNum=0 THEN 
			

		DELETE FROM employee WHERE JMB = pJMB;
		DELETE FROM person WHERE JMB = pJMB;
        set successful=1;
        set message="employee deleted successfully.";
         ELSE
        set successful=0;
        set message="employee can't be deleted.";
    END IF;
    ELSE
        set successful=0;
        set message="employee can't be deleted.";
    END IF;

end$$
delimiter ;

delimiter $$
create procedure delete_package(in aID int, out successful int, out message varchar(255))
begin

    DECLARE resId INT;
    DECLARE packageNum INT;
    DECLARE reservationNum INT;
    DECLARE pOffersHNum INT;


    SELECT COUNT(*) INTO packageNum FROM package WHERE PackageId = aID;
    SELECT COUNT(*) INTO reservationNum FROM Reservation WHERE PackageId = aID;
    SELECT COUNT(*) INTO pOffersHNum FROM package_offers_hotel WHERE PackageId = aID;

    IF packageNum>0 THEN
		IF reservationNum=0 THEN  
          IF pOffersHNum>0 THEN
			DELETE FROM package_offers_hotel WHERE PackageId = aID;
		  END IF;
        DELETE FROM package WHERE PackageId = aID;
        set successful=1;
        set message="package deleted successfully.";
         ELSE
        set successful=0;
       set message="package can't be deleted.";
       END IF;
    ELSE
        set successful=0;
   set message="package can't be deleted.";
    END IF;

end$$
delimiter ;

delimiter $$
create procedure delete_reservation(in rID int, out successful int, out message varchar(255))
begin

    DECLARE reservationNum INT;
    DECLARE PPNum INT;
    DECLARE HPNum INT;
    
    SELECT COUNT(*) INTO reservationNum FROM Reservation WHERE ReservationId = rID;
    SELECT COUNT(*) INTO PPNum FROM package_payment WHERE ReservationId = rID;
    SELECT COUNT(*) INTO HPNum FROM hotel_payment WHERE ReservationId = rID;

	IF reservationNum>0 then
		IF PPNum=0 AND HPNum=0 THEN    
				DELETE FROM Reservation WHERE ReservationId = rID;
        set successful=1;
        set message="Reservation deleted successfully.";
		ELSE
        set successful=0;
        set message="Reservation can't be deleted.";
         END IF;
	ELSE
        set successful=0;
   set message="Reservation can't be deleted.";
    END IF;

end$$
delimiter ;

delimiter $$
create procedure add_reservation(in rPrice decimal(7.2), in rPackageId int, in rHotelId int, in CustomerJMB char(13),
in ReservationPayed tinyint, in rEmployeeJMB char(13), out successful int,  out ResId int,  out message varchar(255))
begin
	declare PostCodePackage int;
    declare PostCodeHotel int;
    declare roomsAvailable int;
    declare resId int;
    declare last_id int;
    
     set ResId = 0;
    
    select PostCode into PostCodePackage
    from package
    where PackageId = rPackageId;
    
    select PostCode into PostCodeHotel
    from hotel
    where HotelId = rHotelId;
    
    select RoomCount into roomsAvailable
    from hotel
    where HotelId = rHotelId;

	if PostCodeHotel <> PostCodePackage then
		set successful=0;
		set message="The requested hotel is not located in the destination offered by this arrangement.";
	elseif roomsAvailable = 0 then
		set successful=0;
		set message="The requested hotel has no rooms available.";
    else
		insert into Reservation (Price, IsPayed, PackageId, HotelId, CustomerJMB, EmployeeJMB)  values (rPrice, ReservationPayed, rPackageId, rHotelId, CustomerJMB, rEmployeeJMB);
		SET last_id = LAST_INSERT_ID();
        
        if ReservationPayed = 1  then
            insert into package_payment (PaymentDate, Amount, ReservationId) values (current_date(), rPrice, last_id);
			insert into hotel_payment (PaymentDate, Amount, ReservationId) values (current_date(), rPrice*0.8, last_id);
            end if; 
        set successful=1;
        set ResId = last_id;
		set message="Reservation successful.";
    end if;
end$$
delimiter ;

delimiter $$
create procedure add_package(in aStartDate date, in aEndDate date, in aPrice decimal(7.2), in aAbout varchar(2000),
in aPostCode int(11), in aDestinationName varchar(255), out PackId int, out successful bool, out message varchar(255))
begin
    declare last_id int;

        insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values (aStartDate, aEndDate, aPrice, aAbout, aPostCode, aDestinationName);
        SET last_id = LAST_INSERT_ID();
        set PackId = last_id;
		set successful=1;
		set message="Package successfully created.";
end$$
delimiter ;

delimiter $$
create procedure add_package_offers_hotel(in rPackageId int, in rHotelId int, out successful int, out message varchar(255))
begin
    declare PostCodePackage int;
    declare PostCodeHotel int;
    declare thereIs int;
    
    select PostCode into PostCodePackage
    from package
    where PackageId = rPackageId;
    
    select PostCode into PostCodeHotel
    from hotel
    where HotelId = rHotelId;
    
	SELECT COUNT(*) INTO thereIs FROM package_offers_hotel WHERE HotelId = rHotelId AND PackageId = rPackageId;

    
    if PostCodeHotel <> PostCodePackage then
		set successful=0;
		set message="The requested hotel is not located in the destination offered by this arrangement.";
	elseif thereIs > 0 then
		set successful=0;
		set message="Entry already exists.";
		
	else
		insert into package_offers_hotel (PackageId, HotelId) values (rPackageId, rHotelId);
        set successful =1;
		set message="Hotel added successfully.";
        end if;
end$$
delimiter ;

delimiter $$

CREATE PROCEDURE change_theme (in p_EmplyeeJMB char(13), in p_NewTheme VARCHAR(20), out successful int, out message varchar(255))
BEGIN
    -- Provjera da li korisnik postoji
    IF EXISTS (SELECT 1 FROM employee WHERE JMB = p_EmplyeeJMB) THEN
        -- Ažuriraj preferiranu temu korisnika
        UPDATE employee
        SET PreferredTheme = p_NewTheme
        WHERE JMB = p_EmplyeeJMB;
        
        set successful=1;
		set message="Theme updated successfully.";
    ELSE
        set successful=0;
		set message="User not found.";
    END IF;
END$$
delimiter ;

delimiter $$
create trigger reduce_room_count after insert
on Reservation
for each row
begin
  
	declare id_exists Boolean;
	select 1
	into @id_exists
	from hotel
	where hotel.HotelId = NEW.HotelId;

       if @id_exists = 1 then
		update hotel
		set RoomCount=RoomCount-1
		where HotelId=new.HotelId;
	end if;
end$$
delimiter ;


call travel_agency.add_customer('0102551555616', 'Sanja', 'Mišić', 'Ulica 51', '2001-01-01', 'sanjamisic@gmail.com', '+38765498123', @successful, @message);
call travel_agency.add_customer('1607001557616', 'Neven', 'Mišić', 'Ulica 51', '2001-07-16', 'nevenmisic@gmail.com', '+38765579336', @successful, @message);
call travel_agency.add_customer('5149875621345', 'Mia', 'Marić', 'Ulica 11', '2015-01-01', 'miamaric@gmail.com', '+38765777123', @successful, @message);
call travel_agency.add_customer('6595485756213', 'Anita', 'Aćić', 'Ulica 1', '2005-01-01', 'anitaacic@gmail.com', '+38765498123', @successful, @message);


call travel_agency.add_reservation(900, 2, 2, '1407964123117', 0, '1806994126336', @successful, @ResId, @message);
call travel_agency.add_reservation(900, 2, 2, '1607001557616', 0, '1806994126336', @successful, @ResId, @message);
call travel_agency.add_reservation(1600, 3, 3, '0102551555616', 1, '1806994126336', @successful, @ResId, @message);
call travel_agency.add_reservation(1255, 5, 6, '1210987255018', 0, '1806994126336', @successful, @ResId, @message);
call travel_agency.add_reservation(2800, 4, 5, '0102551555616', 0, '1806994126336', @successful, @ResId, @message);

call travel_agency.pay_reservation(255, 1, @successfulPayment, @message);
call travel_agency.pay_reservation(2255, 2, @successfulPayment, @message);
call travel_agency.pay_reservation(255, 3, @successfulPayment, @message);
call travel_agency.pay_reservation(280, 2, @successfulPayment, @message);
call travel_agency.pay_reservation(1255, 1, @successfulPayment, @message);
call travel_agency.pay_reservation(320, 3, @successfulPayment, @message);
call travel_agency.pay_reservation(400, 4, @successfulPayment, @message);
call travel_agency.pay_reservation(400, 5, @successfulPayment, @message);

call travel_agency.change_theme('6598744126336', 'bordo',@successful, @message);

insert into person (JMB, FirstName, LastName, Address, DateOfBirth, Email, PhoneNumber) values 
('1208983126117', 'Marko', 'Jovanović', 'Ulica 5', '1990-05-21', 'markojovanovic@gmail.com', '+387661234567'),
('1208983126118', 'Ivana', 'Petrović', 'Ulica 7', '1985-07-15', 'ivanapetrovic@gmail.com', '+387665678123'),
('1208983126119', 'Nikola', 'Milić', 'Ulica 9', '1993-11-02', 'nikolamilic@gmail.com', '+387662345678'),
('1208983126120', 'Jelena', 'Kovačević', 'Ulica 12', '1988-04-09', 'jelenakovacevic@gmail.com', '+387663456789'),
('1208983126121', 'Stefan', 'Radić', 'Ulica 14', '1991-12-30', 'stefanradic@gmail.com', '+387664567890'),
('1208983126122', 'Milan', 'Popović', 'Ulica 16', '1987-06-25', 'milanpopovic@gmail.com', '+387665678901'),
('1208983126123', 'Sandra', 'Marić', 'Ulica 18', '1992-09-14', 'sandramaric@gmail.com', '+387666789012'),
('1208983126124', 'Dragan', 'Vasić', 'Ulica 20', '1994-01-05', 'draganvasic@gmail.com', '+387667890123'),
('1208983126125', 'Aleksandra', 'Stojanović', 'Ulica 22', '1995-03-18', 'aleksandrastojanovic@gmail.com', '+387668901234');

insert into customer (JMB) values ('1208983126117'), ('1208983126118'), ('1208983126120'),
 ('1208983126119'), ('1208983126121'), ('1208983126122');


insert into employee (JMB, Salary, Username, PasswordString, EmploymentDate, RoleType) values 
('1208983126123', 1800.00, 'admin2', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=', '2020-10-01', 'admin'),
('1208983126124', 1800.00, 'korisnickoIme7', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=', '2010-10-01', 'salesAgent'),
('1208983126125', 1800.00, 'korisnickoIme6', 'm4dppKdClZotApjDb7cGI/LfrNqENiN98I2N/Vs3N0w=', '2022-10-01', 'salesAgent');


insert into country (CountryName) values  ('Španija'), ('Kolumbija'), ('Njemačka');


insert into destination (PostCode, DestinationName, About, Distance, LocalLanguage, CountryName) values
 (1, 'Minhen', 'Putovanje u Minhen.', 814, 'njemacki', 'Njemačka'),
 (2, 'Split', 'Putovanje u Split.', 814, 'hrvatski', 'Hrvatska');


insert into hotel (Name_of, RoomCount, Address, Email, ContainsRestaurant, PostCode, DestinationName) values
('In n out', 15, 'Adresa 1335', 'reservation@innouthotel.com', 1, 1, 'Minhen'),
('Minih', 22, 'Adresa 100', 'reservation@minihhotel.com', 1, 1, 'Minhen'),
('Posejdon', 10, 'Adresa 4', 'reservation@posejdonhotel.com', 0, 2, 'Split'),
('Posejdon 2', 45, 'Adresa 5', 'reservation@posejdon2hotel.com', 1, 2, 'Split'),
('Posejdon 3', 23, 'Adresa 6', 'reservation@posejdon3hotel.com', 1, 2, 'Split');


insert into package (StartDate, EndDate, Price, About, PostCode, DestinationName) values
('2024-09-03', '2024-09-10', 1300.00, 'Sedam dana u Minhenu.', 1, 'Minhen'),
('2024-09-25', '2024-09-30', 1199.00, 'Pet dana u Minhenu.', 1, 'Minhen'),
('2025-06-03', '2025-06-10', 1999.00, 'Sedam dana u Splitu.', 2, 'Split'),
('2025-04-03', '2025-04-10', 1999.00, 'Sedam dana u Splitu.', 2, 'Split'),
('2025-08-03', '2025-08-10', 2500.00, 'Sedam dana u Splitu.', 2, 'Split'),
('2025-07-03', '2025-07-10', 3100.00, 'Sedam dana u Splitu.', 2, 'Split');


insert into package_offers_hotel (PackageId, HotelId) values 
(6, 8), (6, 9), (7, 9), (7, 8), (8, 10), (8, 11), (8, 12), (9, 10), (10, 10);


insert into Reservation (Price, IsPayed, PackageId, HotelId, CustomerJMB, EmployeeJMB) values
(1300.00, false, 6, 8, '1208983126117', '1208983126124'),
(1199.00, false, 7, 9, '1208983126119', '1208983126124'),
(1999.00, false, 8, 10, '1208983126120', '1208983126125');

call travel_agency.pay_reservation(50, 7, @successfulPayment, @message);
call travel_agency.pay_reservation(50, 7, @successfulPayment, @message);
call travel_agency.pay_reservation(50, 7, @successfulPayment, @message);
call travel_agency.pay_reservation(100, 3, @successfulPayment, @message);
call travel_agency.pay_reservation(100, 1, @successfulPayment, @message);


-- call travel_agency.pay_reservation(800, 6, @successfulPayment, @message);

