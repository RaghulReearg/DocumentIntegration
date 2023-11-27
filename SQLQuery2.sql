create table TB_FILE_TYPE_MST(
Id int not null primary key identity(1,1),
FileType nvarchar(200) not null,
Extension nvarchar(100),
CreatedOn Datetime,
ModifiedOn Datetime
)

--select * from TB_FILE_TYPE_MST

create table TB_FILE_DETAILS_MST(
Id int not null primary key identity(1,1),
FileTypeId int foreign key references TB_FILE_TYPE_MST(Id),
FileName nvarchar(200),
FileDescription nvarchar(max),
CreatedOn Datetime,
ModifiedOn Datetime
)

create table TB_FILE_HEADER_CONTENT(
Id int not null primary key identity(1,1),
FileTypeId int foreign key references TB_FILE_DETAILS_MST(Id),
LeftHeader nvarchar(max),
RightHeader nvarchar(max),
MainHeader nvarchar(max),
LeftHeaderLogo nvarchar(max),
RightHeaderLogo nvarchar(max),
MainHeaderLogo nvarchar(max),
CreatedOn Datetime,
ModifiedOn Datetime
)

create table TB_FILE_FOOTER_CONTENT(
Id int not null primary key identity(1,1),
FileTypeId int foreign key references TB_FILE_DETAILS_MST(Id),
LeftFooter nvarchar(max),
RightFooter nvarchar(max),
MainFooter nvarchar(max),
LeftFooterLogo nvarchar(max),
RightFooterLogo nvarchar(max),
MainFooterLogo nvarchar(max),
CreatedOn Datetime,
ModifiedOn Datetime
)