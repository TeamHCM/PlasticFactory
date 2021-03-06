USE [PlasticFactory]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Sex] [nvarchar](5) NULL,
	[Phone] [nchar](15) NULL,
	[Type] [int] NOT NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_Customer_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[MSNV] [char](10) NOT NULL,
	[Hoten] [nvarchar](50) NULL,
	[Ngaysinh] [datetime] NULL,
	[Gioitinh] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
	[SDT] [nchar](15) NULL,
	[CMND] [nchar](15) NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_Employee_isDelete]  DEFAULT ((0)),
	[Type] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[MSNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeePayment]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeePayment](
	[ID] [int] NOT NULL,
	[DATE] [date] NULL,
	[MSNV] [char](10) NULL,
	[PAY] [float] NOT NULL CONSTRAINT [DF_EmployeePayment_PAY]  DEFAULT ((0)),
	[NEBT] [float] NOT NULL CONSTRAINT [DF_EmployeePayment_NEBT]  DEFAULT ((0)),
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_EmployeePayment_isDelete]  DEFAULT ((0)),
	[ProductPrice] [int] NOT NULL CONSTRAINT [DF_EmployeePayment_ProductPrice]  DEFAULT ((0)),
	[TimePrice] [int] NOT NULL CONSTRAINT [DF_EmployeePayment_TimePrice]  DEFAULT ((0)),
	[Wage] [float] NOT NULL CONSTRAINT [DF_EmployeePayment_Wage]  DEFAULT ((0)),
	[Cash] [float] NOT NULL CONSTRAINT [DF_EmployeePayment_Cash]  DEFAULT ((0)),
	[isPayed] [bit] NOT NULL CONSTRAINT [DF_EmployeePayment_isPayed]  DEFAULT ((0)),
	[MonthOfPay] [int] NOT NULL CONSTRAINT [DF_EmployeePayment_MonthOfPay]  DEFAULT ((0)),
	[YearOfPay] [int] NOT NULL CONSTRAINT [DF_EmployeePayment_YearOfPay]  DEFAULT ((0)),
 CONSTRAINT [PK_EmployeePayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeType]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_EmployeeType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeWage]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeWage](
	[ID] [int] NOT NULL,
	[Wage] [float] NULL,
 CONSTRAINT [PK_EmployeeWage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentInput]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentInput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[MSDH] [int] NULL,
	[Payment] [int] NULL,
	[Own] [int] NULL,
	[isDelete] [bit] NULL CONSTRAINT [DF_PaymentInput_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_PaymentInput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentOutput]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentOutput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[MSDH] [int] NULL,
	[Payment] [int] NULL,
	[Own] [int] NULL,
	[isDelete] [bit] NULL CONSTRAINT [DF_PaymentOutput_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_PaymentOutput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PreferceProductPrice]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreferceProductPrice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_PreferceProductPrice_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_PreferceProductPrice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductInput]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[MSKH] [int] NOT NULL,
	[MSXE] [text] NULL,
	[TruckWeight] [int] NULL,
	[ProductName] [nvarchar](50) NULL,
	[ProductWeight] [int] NULL,
	[ProductPrice] [int] NULL,
	[TotalAmount] [int] NOT NULL CONSTRAINT [DF_ProductInput_TotalAmount]  DEFAULT ((0)),
	[Payed] [int] NOT NULL CONSTRAINT [DF_ProductInput_Payed]  DEFAULT ((0)),
	[Own] [int] NOT NULL CONSTRAINT [DF_ProductInput_Own]  DEFAULT ((0)),
	[Paid] [bit] NOT NULL CONSTRAINT [DF_ProductInput_Paid]  DEFAULT ((0)),
	[TotalWeight] [int] NOT NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_ProductInput_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_ProductInput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductIP]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductIP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_Product_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductOP]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[isDelete] [bit] NOT NULL,
 CONSTRAINT [PK_ProductOP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductOutput]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOutput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[MSKH] [int] NULL,
	[MSXE] [text] NULL,
	[TruckWeight] [int] NULL,
	[ProductName] [nvarchar](50) NULL,
	[ProductWeight] [int] NULL,
	[ProductPrice] [int] NULL,
	[TotalAmount] [int] NULL,
	[Payed] [int] NOT NULL CONSTRAINT [DF_ProductOutput_Payed]  DEFAULT ((0)),
	[Own] [int] NOT NULL CONSTRAINT [DF_ProductOutput_Own]  DEFAULT ((0)),
	[Paid] [bit] NOT NULL CONSTRAINT [DF_ProductOutput_Paid]  DEFAULT ((0)),
	[TotalWeight] [int] NOT NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_ProductOutput_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_ProductOutput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Timekeeping]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Timekeeping](
	[MSNV] [char](10) NOT NULL,
	[Date] [date] NULL,
	[TimeStart] [text] NULL,
	[TimeEnd] [text] NULL,
	[Time] [float] NOT NULL,
	[Weight] [int] NULL,
	[Type] [int] NULL,
	[TotalWeight] [int] NULL,
	[AdvancePayment] [float] NULL,
	[Note] [nvarchar](200) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_Timekeeping_isDelete]  DEFAULT ((0)),
	[Food] [float] NOT NULL CONSTRAINT [DF_Timekeeping_Food]  DEFAULT ((0)),
	[Punish] [float] NOT NULL CONSTRAINT [DF_Timekeeping_Punish]  DEFAULT ((0)),
	[Bunus] [float] NOT NULL CONSTRAINT [DF_Timekeeping_Bunus]  DEFAULT ((0)),
	[isRest] [bit] NOT NULL CONSTRAINT [DF_Timekeeping_isRest]  DEFAULT ((0)),
 CONSTRAINT [PK_Timekeeping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Truck]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Truck](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LicencePlate] [nchar](10) NULL,
	[Weight] [int] NULL,
	[MSKH] [int] NOT NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_Truck_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_Truck] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeofCustomer]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeofCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[isDelete] [bit] NOT NULL CONSTRAINT [DF_TypeofCustomer_isDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_TypeofCustomer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeWeight]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeWeight](
	[Type] [int] IDENTITY(1,1) NOT NULL,
	[KG] [int] NULL,
 CONSTRAINT [PK_TypeWeight] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProductOP] ADD  CONSTRAINT [DF_ProductOP_isDelete]  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_TypeofCustomer] FOREIGN KEY([Type])
REFERENCES [dbo].[TypeofCustomer] ([ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_TypeofCustomer]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY([Type])
REFERENCES [dbo].[EmployeeType] ([ID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeeType]
GO
ALTER TABLE [dbo].[EmployeePayment]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePayment_Employee] FOREIGN KEY([MSNV])
REFERENCES [dbo].[Employee] ([MSNV])
GO
ALTER TABLE [dbo].[EmployeePayment] CHECK CONSTRAINT [FK_EmployeePayment_Employee]
GO
ALTER TABLE [dbo].[PaymentInput]  WITH CHECK ADD  CONSTRAINT [FK_PaymentInput_ProductInput] FOREIGN KEY([MSDH])
REFERENCES [dbo].[ProductInput] ([ID])
GO
ALTER TABLE [dbo].[PaymentInput] CHECK CONSTRAINT [FK_PaymentInput_ProductInput]
GO
ALTER TABLE [dbo].[PaymentOutput]  WITH CHECK ADD  CONSTRAINT [FK_PaymentOutput_ProductOutput] FOREIGN KEY([MSDH])
REFERENCES [dbo].[ProductOutput] ([ID])
GO
ALTER TABLE [dbo].[PaymentOutput] CHECK CONSTRAINT [FK_PaymentOutput_ProductOutput]
GO
ALTER TABLE [dbo].[ProductInput]  WITH CHECK ADD  CONSTRAINT [FK_ProductInput_Customer] FOREIGN KEY([MSKH])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[ProductInput] CHECK CONSTRAINT [FK_ProductInput_Customer]
GO
ALTER TABLE [dbo].[ProductOutput]  WITH CHECK ADD  CONSTRAINT [FK_ProductOutput_Customer] FOREIGN KEY([MSKH])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[ProductOutput] CHECK CONSTRAINT [FK_ProductOutput_Customer]
GO
ALTER TABLE [dbo].[Truck]  WITH CHECK ADD  CONSTRAINT [FK_Truck_Truck] FOREIGN KEY([MSKH])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Truck] CHECK CONSTRAINT [FK_Truck_Truck]
GO
/****** Object:  StoredProcedure [dbo].[AutoIdEmployee]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AutoIdEmployee]
AS
DECLARE @TEMP INT=1
DECLARE @MSNV NCHAR(5)='NV'+FORMAT(@TEMP,'d3');
DECLARE @CHECK INT=0;
WHILE(@CHECK!=1)
BEGIN
IF EXISTS(SELECT*FROM Employee e WHERE E.MSNV=@MSNV)
BEGIN 
SET @TEMP=@TEMP+1;
SET @MSNV='NV'+FORMAT(@TEMP,'d3');
END
ELSE
BEGIN
SELECT 'AutoMSNV'=@MSNV
BREAK;
END
END


GO
/****** Object:  Trigger [dbo].[deleteofCuatomer]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[deleteofCuatomer] on
[dbo].[Customer]
for update 
as
begin
declare @mskh int ;
select @mskh=i.ID from inserted i where i.isDelete=1;
delete from Truck where MSKH=@mskh;
end
GO
/****** Object:  Trigger [dbo].[EmployeePaymentIsPayed]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[EmployeePaymentIsPayed] ON [dbo].[EmployeePayment]
FOR INSERT,UPDATE
AS
DECLARE 
@TotalTime int,
@ToTalProduct int,
@Cash float,
@Dept float,
@Pay float,
@Payed float,
@Food float,
@Bonus float,
@Punish float,
@MSNV NCHAR(10),
@Wage float,
@Month int,
@Year int,
@TimeKeepingMonth int,
@TimeKeepingYear int,
@MONEYPRODUCT INT,
@MONEYTIME INT,
@TYPEEMPLOYEE INT;
SET @Dept=0
SELECT @MSNV=I.MSNV,@Month=MONTH(I.DATE),@Year=YEAR(I.DATE),@TimeKeepingMonth=MonthOfPay,@TimeKeepingYear=YearOfPay,@Wage=Wage FROM inserted I
SELECT @TotalTime=Sum(T.Time),@ToTalProduct=Sum(T.TotalWeight),@Cash=SUM(T.AdvancePayment),@Food=SUM(T.Food),@Bonus=SUM(T.Bunus),@Punish=SUM(T.Punish) FROM [dbo].[Timekeeping] T WHERE t.isDelete=0 AND T.MSNV=@MSNV AND MONTH(T.Date)=@TimeKeepingMonth AND YEAR(t.Date)=@TimeKeepingYear;  
--KIEM TRA DEPT
--ROI VAO THANG 1
if(@TimeKeepingMonth=1)
BEGIN
DECLARE @COUNT INT
SELECT @COUNT=COUNT(*) FROM  [dbo].[EmployeePayment] WHERE MSNV=@MSNV AND MonthOfPay=12 AND YearOfPay=@Year-1;
IF(@COUNT!=0)
BEGIN
SELECT @Dept=NEBT FROM  [dbo].[EmployeePayment] WHERE MSNV=@MSNV AND MonthOfPay=12 AND YearOfPay=@Year-1;
END
END
--ROI VAO CAC THANG KHAC
Else
BEGIN
DECLARE @COUNT1 INT
SELECT @COUNT1=COUNT(*) FROM  [dbo].[EmployeePayment] WHERE MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth-1 AND YearOfPay=@TimeKeepingYear-1;
IF(@COUNT1!=0)
BEGIN
SELECT @Dept=NEBT FROM  [dbo].[EmployeePayment] WHERE MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth-1 AND YearOfPay=@TimeKeepingYear-1;
END
END
--KET THUC DEPT
SELECT @Payed=SUM(PAY) FROM [dbo].[EmployeePayment] WHERE isDelete=0 AND MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth AND YearOfPay=@TimeKeepingYear; 
SELECT @MONEYTIME=TimePrice,@MONEYPRODUCT=ProductPrice FROM [dbo].[EmployeePayment] WHERE isDelete=0 AND MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth AND YearOfPay=@TimeKeepingYear;  
--kiem tra loai nhan vien
SELECT @TYPEEMPLOYEE=Type FROM Employee WHERE MSNV=@MSNV
--lam theo thang
IF(@TYPEEMPLOYEE=1)
BEGIN 
SET @Pay=@Wage+@Bonus-@Punish-@Food-@Cash-@Dept-@Payed;
END
IF(@TYPEEMPLOYEE=2)
BEGIN
SET @Pay=@TotalTime*@MONEYTIME+@ToTalProduct*@MONEYPRODUCT+@Bonus-@Punish-@Food-@Cash-@Dept-@Payed;
END
IF(@Pay<=0)
BEGIN
UPDATE[dbo].[EmployeePayment] SET isPayed=1 WHERE isDelete=0 AND MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth AND YearOfPay=@TimeKeepingYear;
END
IF(@Pay>0)
BEGIN
UPDATE[dbo].[EmployeePayment] SET isPayed=0 WHERE isDelete=0 AND MSNV=@MSNV AND MonthOfPay=@TimeKeepingMonth AND YearOfPay=@TimeKeepingYear;
END

GO
/****** Object:  Trigger [dbo].[PAYMENT_PRODUCTINPUT]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[PAYMENT_PRODUCTINPUT] ON [dbo].[PaymentInput] FOR INSERT,UPDATE
AS
DECLARE @MAHD INT,@PAY INT;
SELECT @MAHD=[MSDH],@PAY=[Payment] FROM inserted ;
SELECT @PAY=ISNULL(SUM(Payment),0) FROM PaymentInput WHERE MSDH=@MAHD AND isDelete=0
Update ProductInput SET Payed=@PAY where ID=@MAHD
GO
/****** Object:  Trigger [dbo].[PAYMENT_PRODUCTOUTPUT]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[PAYMENT_PRODUCTOUTPUT] ON [dbo].[PaymentOutput] FOR INSERT,UPDATE
AS
DECLARE @MAHD INT,@PAY INT;
SELECT @MAHD=[MSDH],@PAY=[Payment] FROM inserted ;
SELECT @PAY=ISNULL(SUM(Payment),0) FROM PaymentOutput WHERE MSDH=@MAHD AND isDelete=0
Update ProductOutput SET Payed=@PAY where ID=@MAHD

GO
/****** Object:  Trigger [dbo].[UPDATE_PAID]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[UPDATE_PAID] ON [dbo].[ProductInput]
FOR UPDATE
AS
DECLARE @MSHD INT
SELECT @MSHD=ID FROM inserted
IF(EXISTS (SELECT* FROM ProductInput WHERE ID=@MSHD AND TotalAmount=Payed))
BEGIN  
UPDATE ProductInput SET Paid=1 where ID=@MSHD;
END
ELSE
BEGIN
UPDATE ProductInput SET Paid=0 where ID=@MSHD;
END

GO
/****** Object:  Trigger [dbo].[UPDATE_PAID_OUT]    Script Date: 09/11/2018 8:12:56 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[UPDATE_PAID_OUT] ON [dbo].[ProductOutput]
FOR UPDATE
AS
DECLARE @MSHD INT
SELECT @MSHD=ID FROM inserted
IF(EXISTS (SELECT* FROM ProductInput WHERE ID=@MSHD AND TotalAmount=Payed))
BEGIN  
UPDATE ProductOutput SET Paid=1 where ID=@MSHD;
END
ELSE
BEGIN
UPDATE ProductOutput SET Paid=0 where ID=@MSHD;
END

GO
