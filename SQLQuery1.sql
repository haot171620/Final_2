
create table CONTACT(
	idContact int identity(1,1) primary key,
	tenNguoidung nvarchar(100),	
	tenContact nvarchar(100),
	noidungContact nvarchar(max),		
	sdt int,	
)