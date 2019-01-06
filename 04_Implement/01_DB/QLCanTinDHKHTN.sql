CREATE DATABASE QLCanTin
GO

USE QLCanTin
GO

set dateformat dmy;

SET ANSI_WARNINGS  ON;
-- Your insert TSQL here.
--SET ANSI_WARNINGS ON;

ALTER DATABASE QLCanTin SET ENABLE_BROKER

CREATE TABLE EMPLOYEE
(
    ID nchar(20) PRIMARY KEY,
    
    PASSWORD nchar(20) NOT NULL,
	FULLNAME nchar(50),
	GENDER nchar(10),
	YEAROFBIRTH int,
	PHONE nchar (15),
	EMAIL nchar (50),
	IMAGELINK nchar(100),
	POSITION nchar(50) NOT NULL,
	STATUS nchar(20) NOT NULL
)
GO

CREATE TABLE CUSTOMER
(
	ID nchar(20) PRIMARY KEY,
    
    PIN nchar(30) NOT NULL,
	FULLNAME nchar(50),
	GENDER nchar(10),
	YEAROFBIRTH int,
	PHONE nchar (15),
	EMAIL nchar (50),
	CASH int,
	POINT int,
	IMAGELINK nchar(100),
    STAR int
)
GO


CREATE TABLE FOOD
(	
	ID nchar(20) PRIMARY KEY,
	
	FOODNAME nchar(200),
	FOODTYPE int NOT NULL,
	FOODDESCRIPTION nchar(200),
	PRICE int NOT NULL,
	SALE int,
	IMAGELINK nchar(100),
	STAR int,
	STATUS nchar(50)NOT NULL
)
GO

CREATE TABLE ORDERINFO
(
	ID	nchar(20) PRIMARY KEY,
	
	CUSTOMERID nchar(20),
	EMPLOYEEID nchar(20),
	ORDERDATE datetime,
	TOTALMONEY int,
	STATUS nchar(20) NOT NULL,
	
	FOREIGN KEY(CUSTOMERID) REFERENCES CUSTOMER(ID),
	FOREIGN KEY(EMPLOYEEID) REFERENCES EMPLOYEE(ID)
)
GO

CREATE TABLE ORDERDETAIL
(	
	ORDERID nchar(20),
	FOODID nchar(20),
	
	QUANTITY int NOT NULL,
	TOTALMONEY int,
	COMPLETIONDATE datetime,
	STATUS nchar(20) NOT NULL,

	PRIMARY KEY (ORDERID, FOODID),
	FOREIGN KEY(ORDERID) REFERENCES ORDERINFO(ID),
	FOREIGN KEY(FOODID) REFERENCES FOOD(ID)
)
GO

INSERT INTO EMPLOYEE(ID,PASSWORD,FULLNAME,GENDER,YEAROFBIRTH,PHONE,EMAIL,IMAGELINK,POSITION,STATUS) VALUES
('ly.nguyen', 'ly', N'Nguyễn Thiên Lý', N'Nam', 1998, '0947161098', 'nguyenmit@gmail.com','\\127.0.0.1\CanteenManagement\ly.nguyen.jpg', N'Thu ngân', N'Đang làm'),
('linh.tran', 'linh', N'Trần Khánh Linh', N'Nữ', 1998, '0947161098', 'trankhanhlinh98@gmail.com','\\127.0.0.1\CanteenManagement\linh.tran.jpg', N'Quản lý', N'Đang làm')

INSERT INTO CUSTOMER(ID,PIN,FULLNAME,GENDER,YEAROFBIRTH,PHONE,EMAIL,CASH,POINT,IMAGELINK,STAR) VALUES
('1612352', 'long', N'Nguyễn Hà Hoàng Long', N'Nam', 1998, '0947161098', 'long@gmail.com', 100000, 20,'\\127.0.0.1\CanteenManagement\1612352.jpg', 3),
('1612300', 'khoa', N'Thái Đăng Khoa', N'Nam',1998, '0947161098', 'khoa@gmail.com', 200000, 100, '\\127.0.0.1\CanteenManagement\1612300.jpg', 3)

INSERT INTO FOOD(ID,FOODNAME,FOODTYPE,FOODDESCRIPTION,PRICE,SALE,IMAGELINK,STAR,STATUS) VALUES
('FOOD01', N'Gà chiên nước mắm', 1, N'Thịt gà chiên nước mắm thêm xà lách cộng rau thơm',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD01.jpg',1, N'Còn'),
('FOOD02', N'Phở bò ', 2, N'Thịt bò tái thêm sợi phở cộng rau thơm',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD02.jpg',1, N'Còn'),
('FOOD03', N'Đậu ve xào thịt', 1, N'Thịt heo xào cùng đâu ve tươi',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD03.jpg',3, N'Còn'),
('FOOD04', N'Ba rọi nướng ', 1, N'Ba rọi nướng đi kèm nước mắm',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD04.jpg', 2,N'Còn'),
('FOOD05', N'Hủ tiếu thịt', 2, N'Sợi hủ tiếu cộng thêm thịt heo',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD05.jpg',5, N'Còn'),
('FOOD06', N'Cháo lòng', 1, N'Cháo trắng thêm lòng, gan, tim heo',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD06.jpg',4, N'Còn'),
('FOOD07', N'Nước string', 3, N'Thịt gà chiên nước mắm thêm xà lách cộng rau thơm',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD07.jpg', 4,N'Còn'),
('FOOD08', N'Hoa thiên lý xào thịt bò', 1 , N'Hoa thiên lý được xào chung với thịt bò',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD08.jpg',3, N'Còn'),
('FOOD09', N'Nước mũ trôm nha đam', 3, N'Nước mủ trôm nha đam bổ sung năng lượng, giúp tỉnh táo',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD09.jpg',2, N'Còn'),
('FOOD10', N'Đậu hủ nhồi thịt', 1 , N'Đậu hủ tươi nhồi với thịt bằm',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD10.jpg',2, N'Còn'),
('FOOD11', N'Trứng chiên', 1, N'Trứng gà chiên lên kèm với rau sống',45000, 10,'\\127.0.0.1\CanteenManagement\FOOD11.png',3, N'Còn'),
('FOOD12', N'Bò cuống lá lốt', 1 , N'Thịt bò được cuống kèm với lá lốt',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD12.jpg',4, N'Còn'),
('FOOD13', N'Tôm kho', 1 , N'Tôm được kho cộng với rau thơm',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD13.jpg',3, N'Còn'),
('FOOD14', N'Gà kho', 1 , N'Gà được kho cộng với rau thơm',10000, 0,'\\127.0.0.1\CanteenManagement\FOOD14.jpg',5, N'Còn')

INSERT INTO ORDERINFO(ID,CUSTOMERID,EMPLOYEEID,ORDERDATE,TOTALMONEY,STATUS) VALUES
('ORD01', '1612352', 'ly.nguyen', '10/11/2018', 55000, N'Đang chờ'),
('ORD02', '1612300', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD03', '1612352', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD04', '1612300', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD05', '1612352', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD06', '1612300', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD07', '1612352', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD08', '1612352', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD09', '1612300', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD10', '1612352', 'ly.nguyen', '10/11/2018', 50000, N'Đang chờ'),
('ORD11', '1612300', 'ly.nguyen', '10/11/2018', 25000, N'Đang chờ'),
('ORD12', '1612352', 'ly.nguyen', '10/11/2018', 30000, N'Đang chờ'),
('ORD13', '1612300', 'ly.nguyen', '10/11/2018', 22500, N'Đang chờ'),
('ORD14', '1612352', 'ly.nguyen', '10/11/2018', 60000, N'Đang chờ')

INSERT INTO ORDERDETAIL(ORDERID,FOODID,QUANTITY,TOTALMONEY, COMPLETIONDATE,STATUS) VALUES
('ORD01', 'FOOD01', 2, 45000,'10/11/2018', N'Đang chờ'),
('ORD01', 'FOOD02', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD02', 'FOOD02', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD02', 'FOOD11', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD03', 'FOOD03', 2, 10000,'10/11/2018', N'Đang chờ'),
('ORD04', 'FOOD04', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD05', 'FOOD05', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD06', 'FOOD06', 3, 10000,'10/11/2018', N'Đang chờ'),
('ORD07', 'FOOD07', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD08', 'FOOD08', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD09', 'FOOD09', 2, 10000,'10/11/2018', N'Đang chờ'),
('ORD10', 'FOOD10', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD11', 'FOOD11', 3, 10000,'10/11/2018', N'Đang chờ'),
('ORD12', 'FOOD12', 2, 10000,'10/11/2018', N'Đang chờ'),
('ORD13', 'FOOD13', 1, 10000,'10/11/2018', N'Đang chờ'),
('ORD14', 'FOOD14', 1, 10000,'10/11/2018', N'Đang chờ')


CREATE TRIGGER orderinfo_Status
ON ORDERDETAIL
FOR UPDATE, INSERT
AS 
	IF UPDATE(STATUS)
	BEGIN
		DECLARE @orderID nchar(20)
		DECLARE @foodID int
		DECLARE @flag int
		DECLARE @price int
		DECLARE @totalMoneyInfo int
		DECLARE @statusOrderInfo nchar(20)
		DECLARE @statusOrderDetail nchar(20)
		
		SET @flag=0
		SELECT @orderID=I.ORDERID FROM INSERTED I
		
		SELECT @statusOrderInfo=STATUS, @totalMoneyInfo=TOTALMONEY FROM ORDERINFO WHERE ID=@orderID
		
		IF(@statusOrderInfo<>'Xong')
		BEGIN
			SELECT *
			INTO   #TempTable
			FROM   ORDERDETAIL WHERE ORDERID=@orderID

			WHILE ((SELECT COUNT(*) FROM #TempTable) > 0)
			BEGIN
				SELECT TOP 1 @foodID=FOODID, @statusOrderDetail=STATUS FROM #TempTable
			
				IF (@statusOrderDetail=N'Đang chờ')	
				BEGIN
					SET @flag=1
					BREAK
				END
					
				DELETE #TempTable WHERE ORDERID=@orderID AND FOODID=@foodID
			END

			DROP TABLE #TempTable

			IF(@flag=1)
				UPDATE ORDERINFO SET STATUS=N'Đang chờ' WHERE ID=@orderID
			ELSE IF(@flag=0)
				UPDATE ORDERINFO SET STATUS=N'Xong' WHERE ID=@orderID
		END
		
		ELSE 
		BEGIN
			SELECT *
			INTO   #Temp
			FROM   ORDERDETAIL WHERE ORDERID=@orderID

			WHILE ((SELECT COUNT(*) FROM #Temp) > 0)
			BEGIN
				SELECT TOP 1 @foodID=FOODID, @statusOrderDetail=STATUS, @price=TOTALMONEY FROM #Temp
			
				IF (@statusOrderDetail=N'Hết món')	
				BEGIN
					SET @flag=@flag+@price
				END
					
				DELETE #Temp WHERE ORDERID=@orderID AND FOODID=@foodID
			END

			DROP TABLE #Temp

			IF(@flag>0)
				UPDATE ORDERINFO SET TOTALMONEY=@totalMoneyInfo-@flag WHERE ID=@orderID
		END
	END
GO

CREATE TRIGGER customer_star
ON CUSTOMER
FOR UPDATE, INSERT
AS 
	IF UPDATE(POINT)
	BEGIN
		DECLARE @ID nchar(20)
		DECLARE @point int
		DECLARE @star int
		
		SET @star=1
		
		SELECT @ID=I.ID, @point=I.POINT FROM INSERTED I
		
		-- 100.000 điểm = 1 star
		SET @star=1+@point/100000
		IF (@star>5)
			SET @star=5
		
		UPDATE CUSTOMER SET STAR=@star WHERE ID=@ID
	END
GO

CREATE TRIGGER food_star
ON ORDERDETAIL
FOR UPDATE, INSERT
AS 
	IF UPDATE(FOODID)
	BEGIN
		DECLARE @orderID nchar(20)
		DECLARE @foodID int
		DECLARE @statusOrderDetail nchar(20)
		DECLARE @quantity int
		DECLARE @count int
		DECLARE @star int
		SET @count=0
		
		SELECT @foodID=I.FOODID FROM INSERTED I
		
		SELECT *
		INTO   #TempTable
		FROM   ORDERDETAIL WHERE FOODID=@foodID

		WHILE ((SELECT COUNT(*) FROM #TempTable) > 0)
		BEGIN
			SELECT TOP 1 @orderID=ORDERID, @foodID=FOODID, @statusOrderDetail=STATUS, @quantity=QUANTITY FROM #TempTable
			
			IF (@statusOrderDetail=N'Xong')	
				SET @count=@count+@quantity
				
			DELETE #TempTable WHERE ORDERID=@orderID AND FOODID=@foodID
		END
		
		-- 100 dĩa = 1 star
		SET @star=1+@count/100
		IF(@star>5)
			SET @star=5
		
		UPDATE FOOD SET STAR=@star WHERE ID=@foodID
	END