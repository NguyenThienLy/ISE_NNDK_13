drop database QLCanTin
CREATE DATABASE QLCanTin
GO

USE QLCanTin
GO

set dateformat dmy;

SET ANSI_WARNINGS  ON;
-- Your insert TSQL here.
--SET ANSI_WARNINGS ON;

CREATE TABLE EMPLOYEE
(
    ID nchar(20) PRIMARY KEY,
    
    PASSWORD nchar(20),
	FULLNAME nchar(50),
	GENDER nchar(10),
	YEAROFBIRTH int,
	PHONE nchar (15),
	EMAIL nchar (50),
	POSITION nchar(50),
	ROLE nchar(50) NOT NULL
)
GO

CREATE TABLE CUSTOMER
(
	ID nchar(20) PRIMARY KEY,
    
    PIN nchar(30),
	FULLNAME nchar(50),
	GENDER nchar(10),
	YEAROFBIRTH int,
	PHONE nchar (15),
	EMAIL nchar (50),
	CASH int,
	POINT int,
	RANK nchar(20)
)
GO


CREATE TABLE FOOD
(	
	ID nchar(20) PRIMARY KEY,
	
	FOODNAME nchar(200),
	FOODTYPE nchar(20),
	FOODDESCRIPTION nchar(200),
	PRICE int,
	SALE int,
	STATUS nchar(50)
)
GO

CREATE TABLE ORDERINFO
(
	ID	nchar(20) PRIMARY KEY,
	
	CUSTOMERID nchar(20),
	EMPLOYEEID nchar(20),
	ORDERDATE datetime,
	TOTALMONEY int,
	STATUS nchar(20),
	
	FOREIGN KEY(CUSTOMERID) REFERENCES CUSTOMER(ID),
	FOREIGN KEY(EMPLOYEEID) REFERENCES EMPLOYEE(ID)
)
GO

CREATE TABLE ORDERDETAIL
(	
	ORDERID nchar(20),
	FOODID nchar(20),
	
	QUANTITY int,
	TOTALMONEY int,
	
	PRIMARY KEY (ORDERID, FOODID),
	FOREIGN KEY(ORDERID) REFERENCES ORDERINFO(ID),
	FOREIGN KEY(FOODID) REFERENCES FOOD(ID),
)
GO

INSERT INTO EMPLOYEE(ID,PASSWORD,FULLNAME,GENDER,YEAROFBIRTH,PHONE,EMAIL,POSITION,ROLE) VALUES
('ly.nguyen', 'ly', 'Nguyen Thien Ly', 'Nam',1998, '0947161098', 'nguyenmit@gmail.com', 'Thu ngan', 'Member'),
('linh.tran', 'linh', 'Tran Khanh Linh', 'Nu',1998, '0947161098', 'trankhanhlinh98@gmail.com', 'Quan ly', 'Admin')

INSERT INTO CUSTOMER(ID,PIN,FULLNAME,GENDER,YEAROFBIRTH,PHONE,EMAIL,CASH,POINT,RANK) VALUES
('long.nguyen', 'long', 'Nguyen Ha Hoang Long', 'Nam',1998, '0947161098', 'long@gmail.com', 100000, 20, 'Dong'),
('khoa.thai', 'khoa', 'Thai Dang Khoa', 'Nam',1998, '0947161098', 'khoa@gmail.com', 200000, 100, 'Bac')

INSERT INTO FOOD(ID,FOODNAME,FOODTYPE,FOODDESCRIPTION,PRICE,SALE,STATUS) VALUES
('FOOD01', 'Ga chien nuoc mam', 'Dung trong ngay', '',45000, 10, 'Con'),
('FOOD02', 'Che', 'Dung trong ngay', '',10000, 0, 'Con')

INSERT INTO ORDERINFO(ID,CUSTOMERID,EMPLOYEEID,ORDERDATE,TOTALMONEY,STATUS) VALUES
('ORD01', 'long.nguyen', 'ly.nguyen', '10/11/2018', 55000, 'Dang cho'),
('ORD02', 'khoa.thai', 'ly.nguyen', '10/11/2018', 22500, 'Het mon')

INSERT INTO ORDERDETAIL(ORDERID,FOODID,QUANTITY,TOTALMONEY) VALUES
('ORD01', 'FOOD01', 2, 45000),
('ORD01', 'FOOD02', 1, 10000),
('ORD02', 'FOOD01', 1, 22500)
