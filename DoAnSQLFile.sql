CREATE DATABASE QL_ShopDoChoiTreEm

CREATE TABLE ADMIN
(
	MAAD NCHAR(5) NOT NULL PRIMARY KEY,
	TENAD NVARCHAR(50),
	TENTKAD NVARCHAR(50),
	MATKHAUAD NVARCHAR(50),
	GIOITINH NVARCHAR(5),
	SDT NVARCHAR(50),
	MAIL NVARCHAR(50)
)
CREATE TABLE QUYEN
(
	MAQ NCHAR(5) NOT NULL PRIMARY KEY,
	TENQ nvarchar(20),
	MAAD NCHAR(5) NOT NULL,
	CONSTRAINT FK_PQ_AD FOREIGN KEY (MAAD) REFERENCES ADMIN(MAAD)
)
CREATE TABLE TAIKHOAN
(
	MATK NCHAR(5) NOT NULL,
	TENTK NVARCHAR(50) NOT NULL,
	MATKHAU NVARCHAR(50)NOT NULL,
	PRIMARY KEY(MATK)

)
create table GioiThieu
(
	MaGT int NOT NULL primary key,
	ThongTin nvarchar(3000),
	HinhAnh varchar(50),
	DiaChi nvarchar(100),
	SDT varchar(10),
	Email varchar(50),
	fax varchar(50),
	freeship float,

)
insert into GioiThieu values
(1,N'Là một trong những store mua sắm đồ chơi trẻ em đã và đang được rất nhiều ông bố,
      bà mẹ quan tâm, tin tưởng nhất hiện nay, bởi khi mua sắm tại TB Store quý khách không chỉ 
      tiết kiệm được rất nhiều thời gian mà có thể dễ dàng tìm thấy cho bé đủ các loại
      đồ chơi chất lượng cao từ sơ sinh đến 6 tuổi dành cho cả bé
      trai lẫn bé gái. Các sản phẩm của TB Store rất đa dạng, phong phú, đẹp về kiểu dáng, 
     chất lượng, giá cả cam kết luôn luôn tốt so với thị trường. Các mẫu đồ chơi của 
     TB Store phù hợp cho các bé vui chơi, kích thích sự năng động, tiềm năng phát triển cho các bé.
TB Store rất  hân hạnh trở thành nơi đồng hành đáng tin cậy cho các mẹ và bố .','map3.png',
	 N'140 Lê Trọng Tấn,phường Tây Thạnh, quận Tân Phú, TP.HCM ','0329564744','thanhcanhtran64@gmail.com','(000) 000 00 00 0',500000)

INSERT INTO ADMIN VALUES
('AD01',N'Thanh Cảnh','ThanhCanh64','06041999',N'Nữ','0329564744','thanhcanhtran64@gmail.com'),
('AD02',N'Thành Đạt','Dat123','123',N'Nam','0329564710','nguyenthanhdat21061999@gmail.com')

INSERT INTO QUYEN VALUES
('Q01','Admin','AD01'),
('Q02','Employees','AD02')

CREATE TABLE THUONGHIEU
(
	MATH NCHAR(5) NOT NULL,
	TENTH NVARCHAR(50),
	LOGO NVARCHAR(50),
	PRIMARY KEY (MATH)
)
CREATE TABLE LOAI
(
	MALOAI NCHAR(5) NOT NULL,
	TENLOAI NVARCHAR(50),
	PRIMARY KEY(MALOAI),
)
CREATE TABLE CHATLIEU
(
	MACL NCHAR(5) NOT NULL,
	TENCL NVARCHAR(50),
	PRIMARY KEY(MACL)
)
CREATE TABLE TUOI
(
	MATUOI NCHAR(5) NOT NULL,
	DOTUOI NVARCHAR(50),
	PRIMARY KEY (MATUOI)
)
CREATE TABLE SANPHAM
(
	MASP NCHAR(5) NOT NULL,
	TENSP NVARCHAR(50),
	HINHANH NVARCHAR(50),
	PRIMARY KEY(MASP),
	DONGIA REAL,
	CHITIET NVARCHAR(500),
	MALOAI NCHAR(5),
	CONSTRAINT FK_SANPHAM_LOAI FOREIGN KEY (MALOAI) REFERENCES LOAI(MALOAI),
	MACL NCHAR(5),
	CONSTRAINT FK_SANPHAM_CHATLIEU FOREIGN KEY (MACL) REFERENCES CHATLIEU(MACL),
	MATH NCHAR(5),
	CONSTRAINT FK_SANPHAM_THUONGHIEU FOREIGN KEY (MATH) REFERENCES THUONGHIEU(MATH),
	MATUOI NCHAR(5),
	CONSTRAINT FK_SANPHAM_TUOI FOREIGN KEY (MATUOI) REFERENCES TUOI(MATUOI)
)
CREATE TABLE KHACHHANG
(
	MAKH NCHAR(5) NOT NULL,
	TENKH NVARCHAR(50),
	DIACHI NVARCHAR(50) NOT NULL,
	SDT NCHAR(11) ,
	NAMSINH INT,
	GIOITINH NVARCHAR(5),
	LOAIKH NVARCHAR(50),
	MATK NCHAR(5),
	PRIMARY KEY(MAKH),
	CONSTRAINT FK_KHACHHANG_TAIKHOAN FOREIGN KEY (MATK) REFERENCES TAIKHOAN(MATK)
)
CREATE TABLE HOADON
(
	MAHD NCHAR(5) NOT NULL,
	MAKH NCHAR(5),
	NGAYBAN DATE,
	TIENBAN float,
	GIAMGIA float,
	THANHTIEN float,
	PHISHIP float,
	PRIMARY KEY(MAHD),
	CONSTRAINT FK_HOADON_KHACHHANG FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
)
CREATE TABLE CHITIETHD
(
MAHD NCHAR(5) NOT NULL,
MASP NCHAR(5) NOT NULL,
SOLUONG INT,
DONGIA float,
THANHTIEN float,
PRIMARY KEY(MAHD,MASP),
CONSTRAINT FK_CTHD_HOADON FOREIGN KEY (MAHD) REFERENCES HOADON(MAHD),
CONSTRAINT FK_CTHD_SANPHAM FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)
go
create view PHANQUYEN 
as
select admin.MAAD,admin.TENAD,admin.TENTKAD, admin.MATKHAUAD, admin.GIOITINH, admin.SDT, admin.MAIL, q.TENQ
from ADMIN admin, QUYEN q
where admin.MAAD = q.MAAD


go
create trigger ttHD on HOADON
after insert,update
as
begin
	DECLARE @GG FLOAT,@MAKH NCHAR(5),@LOAIKH NVARCHAR(50),@DC NVARCHAR(50)
	SELECT @MAKH=MAKH FROM inserted
	SELECT @LOAIKH=LOAIKH,@DC=DIACHI FROM KHACHHANG WHERE MAKH=@MAKH
	IF(@LOAIKH='VIP')
		update HOADON
		SET GIAMGIA=100000
		WHERE MAKH=@MAKH
	ELSE
		update HOADON
		SET GIAMGIA=0
		WHERE MAKH=@MAKH
	IF(@DC='TP.HCM')
		update HOADON
		SET PHISHIP=20000
		WHERE MAKH=@MAKH
	ELSE
		update HOADON
		SET PHISHIP=30000
		WHERE MAKH=@MAKH
end
go
create trigger ttct on CHITIETHD
after insert,update
as
begin
	update CHITIETHD
	set DONGIA=(select DONGIA from SANPHAM where MASP=(select MASP from inserted))
	where MASP=(select MASP from inserted) and MAHD=(select MAHD from inserted)
	update CHITIETHD
	set THANHTIEN=DONGIA*SOLUONG
	where  MASP=(select MASP from inserted) and MAHD=(select MAHD from inserted)
	UPDATE HOADON
	SET TIENBAN=(SELECT SUM(THANHTIEN) 
	FROM CHITIETHD 
	WHERE MAHD=(select MAHD from inserted))
	from HOADON
	where MAHD=(select MAHD from inserted)
	UPDATE HOADON
	SET THANHTIEN=TIENBAN+PHISHIP-GIAMGIA
	WHERE MAHD=(select MAHD from inserted)

end
go
INSERT INTO TAIKHOAN VALUES
('TK1','SUSI_123','123456'),
('TK2','BANHTRANG.COM','BANHTRANGTRON'),
('TK3','VUI_64@','64BANHBAO'),
('TK4','THANHDAT','THANHDAT123'),
('TK5','CANH','CANH123'),
('TK6','THANHTHANH','THANH123'),
('TK7','BAOTRAN','TRAN123'),
('TK8','VANGCUC','CUCVANG')

INSERT INTO THUONGHIEU VALUES
('TH1','PEOPLE','th1.png'),
('TH2','RANGS JAPAN','th2.png'),
('TH3','KINETIC SAND','th3.png'),
('TH4','LITTLE LIVE PETS','th4.png'),
('TH5','LEARNING RESOURCES','th5.png'),
('TH6','WIN TOYS','th6.png'),
('TH7','PLAY DOH','th7.png'),
('TH8','LITTLE TIKES','th8.png'),
('TH9','CANPOL BABIES','th9.png'),
('TH10','FARLIN','th10.png'),
('TH11','FISHER PRICES','th11.png'),
('TH12','ECOIFFIER','th12.png'),
('TH13','HOT WHEELS','th13.png'),
('TH14','THOMAS & FRIENDS','th14.png'),
('TH15','VBCARE','th15.png'),
('TH16','KLEIN','th16.png'),
('TH17','DANTOY','th17.png'),
('TH18','BABY ALIVE','th18.png'),
('TH19','Ks KIDS','th19.png'),
('TH20','DOLLS WORLD','th20.png'),
('TH21','JUST FOR CHEF','th21.png'),
('TH22','CRAYOLA','th22.png'),
('TH23','RADIO FLYER','th23.png'),
('TH24','MEGA BLOKS','th24.png'),
('TH25','BABY COLOR','th25.png'),
('TH26','LEGO','th26.png'),
('TH27','MOTION SAND','th27.png'),
('TH28','SHOPKINS','th28.png'),
('TH29','SPEEDY','th29.png'),
('TH30','FEISTY PETS','th3.png')

INSERT INTO LOAI VALUES
('L1',N'XẾP KHỐI-LEGO'),
('L2',N'MÔ HÌNH'),
('L3',N'SÁNG TẠO'),
('L4',N'VẬN ĐỘNG'),
('L5',N'SƯU TẬP'),
('L6',N'HÓA TRANG'),
('L7',N'GIÁO DỤC'),
('L8',N'ĐỘNG VẬT')

INSERT INTO CHATLIEU VALUES
('CL1',N'Nhựa'),
('CL2',N'Gỗ'),
('CL3',N'Sắt'),
('CL4',N'Giấy'),
('CL5',N'Thép'),
('CL6',N'Đồng'),
('CL7',N'Vải bông'),
('CL8',N'Tre,nứa'),
('CL9',N'Đất sét')

INSERT INTO TUOI VALUES
('T1',N'Trẻ sơ sinh'),
('T2',N'1-2 tuổi'),
('T3',N'2-3 tuổi'),
('T4',N'3-4 tuổi'),
('T5',N'4-5 tuổi'),
('T6',N'5-6 tuổi'),
('T7',N'Trên 6 tuổi')

INSERT INTO SANPHAM VALUES
('SP1',N'Mèo bông','sp1.png','790000',N'Chú mèo Bông Manekko Nyanmari nhận biết và ghi lại âm thanh và giọng nói','L8','CL7','TH1','T4')
INSERT INTO SANPHAM VALUES
('SP2',N'Mèo bông','sp1.png','790000',N'Chú mèo Bông Manekko Nyanmari nhận biết và ghi lại âm thanh và giọng nói','L8','CL7','TH1','T5'),
('SP3',N'Mèo bông','sp1.png','790000',N'Chú mèo Bông Manekko Nyanmari nhận biết và ghi lại âm thanh và giọng nói','L8','CL7','TH1','T6'),
('SP4',N'Mèo bông','sp1.png','790000',N'Chú mèo Bông Manekko Nyanmari nhận biết và ghi lại âm thanh và giọng nói','L8','CL7','TH1','T7'),
('SP5',N'Xe buýt mini','sp2.png','150000',N'Vỉ 4 chiếc xe buýt sắt trớn 22003-TP','L2','CL3','TH2','T5'),
('SP6',N'Xe buýt mini','sp2.png','150000',N'Vỉ 4 chiếc xe buýt sắt trớn 22003-TP','L2','CL3','TH2','T6'),
('SP7',N'Xe buýt mini','sp2.png','150000',N'Vỉ 4 chiếc xe buýt sắt trớn 22003-TP','L2','CL3','TH2','T7'),
('SP8',N'BỘ ĐỒ CHƠI XẾP KHỐI MÔ HÌNH GẠCH','sp3.png','2500000',N'BỘ ĐỒ CHƠI XẾP KHỐI MÔ HÌNH GẠCH NAM CHÂM MAGNA TILES - Nam châm ẩn bên trong, chịu lực va đập mạnh.','L1','CL2','TH3','T1'),
('SP9',N'BỘ ĐỒ CHƠI XẾP KHỐI MÔ HÌNH GẠCH','sp3.png','2500000',N'BỘ ĐỒ CHƠI XẾP KHỐI MÔ HÌNH GẠCH NAM CHÂM MAGNA TILES - Nam châm ẩn bên trong, chịu lực va đập mạnh.','L1','CL2','TH3','T2'),
('SP10',N'PHÁO ĐÀI','sp4.png','14250000',N'Thiết kế theo mô hình pháo đài lịch sử, cho bé thả sức hóa thân vào các nhân vật lịch sử như vua, hoàng hậu, hoàng tử, công chúa, các chiến binh và kị sĩ','L4','CL1','TH4','T6'),
('SP11',N'PHÁO ĐÀI','sp4.png','14250000',N'Thiết kế theo mô hình pháo đài lịch sử, cho bé thả sức hóa thân vào các nhân vật lịch sử như vua, hoàng hậu, hoàng tử, công chúa, các chiến binh và kị sĩ','L4','CL1','TH4','T7'),
('SP12',N'Bộ khung thành','sp5.png','1270000',N'Bộ khung thành Little Tikes sẽ cho bé làm quen dần với thể thao bằng việc di chuyển bóng và sút vào khung thành.','L4','CL3','TH5','T5'),
('SP13',N'XE TRƯỢT SCOOTER','sp6.png','1260000',N'Tay cầm có thể tháo rời ra để cất giữ gọn gàng.Trọng lượng tối đa: xấp xỉ 20kg','L4','CL3','TH6','T7'),
('SP14',N'Bộ bóng rổ','sp7.png','1980000',N'Bộ bóng rổ Little Tikes 120cm mạnh mẽ giúp điều chỉnh chiều cao hoàn hảo cho ngôi sao nhí của bạn!','L4','CL5','TH7','T4'),
('SP15',N'Bộ bóng rổ','sp7.png','1980000',N'Bộ bóng rổ Little Tikes 120cm mạnh mẽ giúp điều chỉnh chiều cao hoàn hảo cho ngôi sao nhí của bạn!','L4','CL5','TH7','T5'),
('SP16',N'Xe chòi chân thể thao','sp8.png','4400000',N'Có tay cầm gắn trên nóc, hỗ trợ việc đẩy bé thuận tiện hơn.Bánh xe xoay 360 độ','L4','CL1','TH8','T2'),
('SP17',N'Bộ gỗ xếp lâu đài','sp9.png','313000',N' Bộ xếp hình lâu đài gồm 50 mảnh gỗ nhiều màu sắc, nhiều hình dạng khác nhau, bé sẽ có thử thách xếp hình Lâu đài, Kim tự tháp, chợ Bến Thành, chùa Một Cột, Nhà thờ...nhằm phát triển sự sáng tạo và óc logic của mình.','L2','CL2','TH9','T4'),
('SP18',N'Bộ gỗ xếp lâu đài','sp9.png','313000',N' Bộ xếp hình lâu đài gồm 50 mảnh gỗ nhiều màu sắc, nhiều hình dạng khác nhau, bé sẽ có thử thách xếp hình Lâu đài, Kim tự tháp, chợ Bến Thành, chùa Một Cột, Nhà thờ...nhằm phát triển sự sáng tạo và óc logic của mình.','L2','CL2','TH9','T5'),
('SP19',N'Bộ đồ chơi nhà bếp','sp10.png','7128000',N'Kích thước (D x R x C):Phần bếp: 119 x 36 x 101 cm,Phần bàn ăn: 96 x 66 x 101 cm','L3','CL1','TH10','T3'),
('SP20',N'Bộ đồ chơi nhà bếp','sp10.png','7128000',N'Kích thước (D x R x C):Phần bếp: 119 x 36 x 101 cm,Phần bàn ăn: 96 x 66 x 101 cm','L3','CL1','TH10','T4'),
('SP21',N'Bộ bếp thời trang','sp11.png','4208000',N'Sản phẩm dùng pin: AAA,Q/C: 97 x 45 x 105 cm','L3','CL1','TH11','T3'),
('SP22',N'Bộ bếp thời trang','sp11.png','4208000',N'Sản phẩm dùng pin: AAA,Q/C: 97 x 45 x 105 cm','L3','CL1','TH11','T4'),
('SP23',N'Bộ đồ chơi làm kem','sp12.png','2100000',N'Không sử dụng pin hay điện, nên rất an toàn cho trẻ em.Giúp bé vận động cơ thể khỏe mạnh hơn và bắt đầu 1 ngày mới tràn đầy niềm vui','L3','CL1','TH12','T5'),
('SP24',N'Bộ đồ chơi làm kem','sp12.png','2100000',N'Không sử dụng pin hay điện, nên rất an toàn cho trẻ em.Giúp bé vận động cơ thể khỏe mạnh hơn và bắt đầu 1 ngày mới tràn đầy niềm vui','L3','CL1','TH12','T6'),
('SP25',N'Bộ đồ chơi nhà bếp 2 giai đoạn','sp13.png','4949000',N'Bộ nhà bếp được thiết kế có thể diều chỉnh độ cao theo 2 giai đoạn phát triển của bé:
Giai đoạn 1: Độ cao phù hợp cho Bé từ mẩu giáo. (Kích thước : 35 x 68 x 106 cm)
Giai đoạn 2: Bếp được điều chỉnh cao hơn phù hợp cho trẻ từ 2-6 tuổi. (Kích thước : 38 x 119 x 83 cm)','L3','CL1','TH13','T3'),
('SP26',N'Bộ đồ chơi nhà bếp 2 giai đoạn','sp13.png','4949000',N'Bộ nhà bếp được thiết kế có thể diều chỉnh độ cao theo 2 giai đoạn phát triển của bé:
Giai đoạn 1: Độ cao phù hợp cho Bé từ mẩu giáo. (Kích thước : 35 x 68 x 106 cm)
Giai đoạn 2: Bếp được điều chỉnh cao hơn phù hợp cho trẻ từ 2-6 tuổi. (Kích thước : 38 x 119 x 83 cm)','L3','CL1','TH13','T4'),
('SP65',N'Bộ xếp gạch nhỏ','sp14.png','170000',N'Từng thanh gỗ được sơn màu sắc tươi tắn, sinh động sẽ là phương pháp hữu ích giúp bé phát triển thị giác tốt và khám phá thế giới sắc màu','L1','CL2','TH14','T6'),
('SP27',N'Bộ xếp gạch nhỏ','sp14.png','170000',N'Từng thanh gỗ được sơn màu sắc tươi tắn, sinh động sẽ là phương pháp hữu ích giúp bé phát triển thị giác tốt và khám phá thế giới sắc màu','L1','CL2','TH14','T7'),
('SP28',N'Bộ đồ chơi gỗ hình quốc kì','sp15.png','242000',N'Đồ chơi bộ cờ quốc gia sẽ giúp bé biết được các loại cờ khác nhau của một số quốc gia trên thế giới','L7','CL2','TH15','T3'),
('SP29',N'Hộp lego sáng tạo','sp16.png','1999000',N'Sản phẩm Số mảnh ghép: 1000 mảnh ghép','L1','CL1','TH16','T5'),
('SP30',N'Hộp lego sáng tạo','sp16.png','1999000',N'Sản phẩm Số mảnh ghép: 1000 mảnh ghép','L1','CL1','TH16','T6'),
('SP31',N'Lego Friends – Xe kéo và cano của andrea','sp17.png','1099000',N'Sản phẩm Số mảnh ghép: 186 mảnh ghép','L1','CL1','TH17','T7'),
('SP32',N'Bộ tìm chữ cái tiếng anh','sp18.png','207000',N'Sản phẩm được làm từ chất liệu gỗ cao cấp hoàn toàn không độc hại, không gây dị ứng','L7','CL2','TH18','T5'),
('SP33',N'Bộ tìm chữ cái tiếng anh','sp18.png','207000',N'Sản phẩm được làm từ chất liệu gỗ cao cấp hoàn toàn không độc hại, không gây dị ứng','L7','CL2','TH18','T6'),
('SP34',N'Bộ tìm chữ cái tiếng anh','sp18.png','207000',N'Sản phẩm được làm từ chất liệu gỗ cao cấp hoàn toàn không độc hại, không gây dị ứng','L7','CL2','TH18','T7'),
('SP35',N'Bộ ghép hình học chữ tiếng anh','sp19.png','112000',N'Bộ ghép hình học chữ tiếng anh sẽ giúp bé ghép được 15 từ tiếng anh qua 15 hình.Sản phẩm Kích thước: 80 x 80 x 75 (mm)','L7','CL2','TH19','T4'),
('SP36',N'Bảng chữ cái tiếng việt','sp20.png','48000',N'Sản phẩm Kích thước: 26.5 X 4x 19.5 cm','L7','CL4','TH20','T4'),
('SP37',N'Bảng chữ cái tiếng việt','sp20.png','48000',N'Sản phẩm Kích thước: 26.5 X 4x 19.5 cm','L7','CL4','TH20','T5'),
('SP38',N'Xếp hình mỵ châu trọng thủy','sp21.png','50000',N'Sản phẩm được làm từ chất liệu giấy cao cấp hoàn toàn không độc hại','L7','CL4','TH21','T5'),
('SP39',N'Xếp hình mỵ châu trọng thủy','sp21.png','50000',N'Sản phẩm được làm từ chất liệu giấy cao cấp hoàn toàn không độc hại','L7','CL4','TH21','T6'),
('SP40',N'Xe jeep điều khiển màu vàng','sp22.png','243000',N'Đồ chơi xe điều khiển từ xa là dòng sản phẩm ưu tú, được làm từ chất liệu an toàn, các góc cạnh được bo tròn không góc','L2','CL1','TH22','T4'),
('SP41',N'Xe biến hình robot điều khiển màu xanh','sp23.png','239000',N'Đồ chơi xe điều khiển từ xa là dòng sản phẩm ưu tú, được làm từ chất liệu an toàn, các góc cạnh được bo tròn không góc','L2','CL1','TH23','T5'),
('SP42',N'Xe nhào lộn điều khiển','sp24.png','129000',N'Đồ chơi xe điều khiển từ xa là dòng sản phẩm ưu tú, được làm từ chất liệu an toàn, các góc cạnh được bo tròn không góc','L2','CL1','TH24','T5'),
('SP43',N'Xe nhào lộn điều khiển','sp24.png','129000',N'Đồ chơi xe điều khiển từ xa là dòng sản phẩm ưu tú, được làm từ chất liệu an toàn, các góc cạnh được bo tròn không góc','L2','CL1','TH24','T6'),
('SP44',N'Vỉ 3 nhân vật siêu anh hùng avengers','sp25.png','93000',N'Sản phẩm sẽ là món quà tuyệt vời dành tặng cho bé yêu nhà bạn trong những dịp đặc biệt','L2','CL1','TH25','T5'),
('SP45',N'Bộ 4 nhân vật super wings đội bay siêu đẳng','sp26.png','130000',N'Sản phẩm Đồ chơi Super wings Đội bay siêu đẳng bay biến hình sẽ là món quà tuyệt vời dành tặng cho bé yêu nhà bạn trong những dịp đặc biệt','L2','CL1','TH26','T6'),
('SP46',N'Bộ 4 nhân vật super wings đội bay siêu đẳng','sp26.png','130000',N'Sản phẩm Đồ chơi Super wings Đội bay siêu đẳng bay biến hình sẽ là món quà tuyệt vời dành tặng cho bé yêu nhà bạn trong những dịp đặc biệt','L2','CL1','TH26','T7'),
('SP48',N'Đàn piano micro','sp27.png','212000',N'Sản phẩm được làm từ chất liệu an toàn không gây độc hại cho trẻ nhỏ','L7','CL1','TH27','T3'),
('SP49',N'Đàn piano xanh dương, chân đứng','sp28.png','219000',N'Sản phẩm được làm từ chất liệu an toàn không gây độc hại cho trẻ nhỏ','L7','CL1','TH28','T3'),
('SP50',N'BỐC TRỨNG DRAGONBALL SUPER KORE CHARA','sp29.png','912000',N'Sản phẩm gồm 8 nhân vật','L5','CL1','TH29','T7'),
('SP51',N'BỘ SƯU TẬP TRỨNG GACHA POKEMON MINI CLIP FIGURE','sp30.png','684000',N'Bộ sưu tập gồm 6 nhân vật','L5','CL1','TH30','T7'),
('SP52',N'BỐC TRỨNG NHÂN VẬT RACCOON AND FOX FIGURE','sp31.png','456000',N'Bộ sưu tập gồm 4 nhân vật','L5','CL1','TH1','T7'),
('SP53',N'BỘ SƯU TẬP TRỨNG GACHA MÓC KHÓA POCKET','sp32.png','1026000',N'Bộ sưu tập gồm 9 nhân vật','L5','CL1','TH1','T7'),
('SP54',N'BỐC TRỨNG MÓC KHÓA DORAEMON NOBITA MOON','sp33.png','456000',N'Bộ sưu tập gồm 4 nhân vật','L5','CL1','TH1','T7'),
('SP55',N'BỐC TRỨNG CUP FIGUER MAGCAT','sp34.png','972000',N'Bộ sưu tập gồm 9 nhân vật','L5','CL1','TH1','T7'),
('SP56',N'BỐC TRỨNG SUZUKI BIKE','sp35.png','684000',N'Bộ sưu tập gồm 6 mô hình','L5','CL1','TH1','T7'),
('SP57',N'ĐỒ CHƠI BỒN TẮM HÌNH ĐỘNG VẬT TRÊN THUYỀN','sp36.png','120000',N'Sản phẩm đồ chơi nhà tắm Canpol Babies hình thú đáng yêu được thiết kế đặc biệt để bé chơi khi đang tắm, giúp bé thích thú khi được tắm gội, cho giờ tắm trở nên đầy màu sắc và âm thanh.','L8','CL1','TH2','T1'),
('SP58',N'NGẬM NƯỚU','sp37.png','252000',N'Miếng cắn răng Mochi giúp làm dịu nướu răng, sẽ làm hài lòng bất kỳ trẻ sơ sinh nào. Nhờ các vân nổi đa dạng trên bề mặt sản phẩm, giúp bé lựa chọn vùng cắn yêu thích, xoa dịu những cơn ngứa nướu khi mọc răng.','L4','CL8','TH3','T1'),
('SP59',N'Bột nặn làm mì, nui, rau củ','sp38.png','350000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L3','CL9','TH4','T3'),
('SP60',N'Bột nặn làm mì, nui, rau củ','sp38.png','350000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L3','CL9','TH4','T4'),
('SP61',N'Bột nặn làm bánh sinh nhật','sp39.png','136000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L3','CL9','TH4','T4'),
('SP62',N'Con quay tốc độ ','sp40.png','56.000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L4','CL6','TH2','T7'),
('SP63',N'Con quay Nado màu vàng','sp41.png','116000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L4','CL6','TH2','T7'),
('SP64',N'Búp bê Babrie phong cách thời trang váy sọc','sp42.png','299000',N'Sản phẩm được làm từ chất liệu an toàn, đã được kiểm định nên không gây hại cho sức khỏe của bé yêu','L2','CL1','TH2','T3')

SET DATEFORMAT DMY
INSERT INTO KHACHHANG VALUES
('KH1',N'Trần Thanh Cảnh',N'TP.HCM','0329564744','1999',N'Nữ','VIP','TK5'),
('KH2',N'Nguyễn Thành Đạt',N'Bình Định','032934679','1999',N'Nam','Thường','TK4'),
('KH3',N'Đặng Vàng Cúc',N'Bình Thuận','0329521876','1996',N'Nữ','VIP','TK8'),
('KH4',N'Trần Ngọc Bảo Trân',N'Vũng Tàu','032456679','1990',N'Nam','Thường','TK1')

SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD1','KH1','20/11/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD2','KH1','22/10/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD3','KH1','30/09/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD4','KH4','10/08/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD5','KH2','01/07/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD6','KH2','01/06/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD7','KH3','01/05/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD8','KH1','01/04/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD9','KH1','01/03/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD10','KH4','01/02/2019',NULL,NULL,NULL,NULL)
SET DATEFORMAT DMY
INSERT INTO HOADON VALUES('HD11','KH4','01/01/2019',NULL,NULL,NULL,NULL)


INSERT INTO CHITIETHD VALUES ('HD1','SP1','1',0,0)
INSERT INTO CHITIETHD VALUES ('HD1','SP11','2',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD2','SP12','2',NULL,NULL)
INSERT INTO CHITIETHD VALUES('HD3','SP12','3',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD3','SP20','1',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD4','SP18','1',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD5','SP19','2',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD6','SP19','3',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD7','SP20','1',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD8','SP30','2',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD9','SP12','1',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD10','SP21','1',NULL,NULL)
INSERT INTO CHITIETHD VALUES ('HD11','SP19','1',NULL,NULL)



insert into TINTUCs(TENTC,MOTA,HINHANH,VT) values
(N'Trước thời bé Sa đã có 5 "cu tí" YouTube triệu fan đình đám, chưa đi học mà kiếm tiền như nước',
N'Không khó để thấy những bạn nhỏ còn chưa vào lớp 1 đã thu hút hàng triệu fan trên YouTube, thậm chí đem về nguồn thu nhập khổng lồ cho gia đình.
Cuối tuần vừa rồi, 2 mẹ con Quỳnh Trần cùng bé Sa đã có dịp trở lại Việt Nam khiến rất nhiều người thích thú, đặc biệt là những fan ruột thường xuyên theo dõi và ủng hộ từng video mới trên kênh YouTube của 2 người. Có thể nói "Quỳnh Trần JP" đang là một hiện tượng YouTube khi bắt trend mukbang nhưng lại vẫn tạo được nét riêng ấn tượng đến từ cách nói chuyện duyên dáng cũng như vẻ đáng yêu của bé Sa đốn tim biết bao người.

Chuyện những cô bé, cậu bé lon ton lững chững vài tuổi đã kịp nổi tiếng ầm ầm cũng không quá xa lạ trên toàn thế giới, thậm chí có thể xếp luôn được thành danh sách đếm ngược Top 5 siêu đình đám với hàng chục triệu sub YouTube và tổng thu nhập ước tính hàng triệu USD cũng là chuyện bình thường.

5. "Boram Tube Vlog" và "Boram Tube ToysReview" - 21,5 triệu sub và 14 triệu sub

4. "Ryans World" - 22,6 triệu sub

3. "Vlad and Nikita" - 29,7 triệu sub

2. "Kids Diana Show" - 39,2 triệu sub

1. "Like Nastya Vlog", "Stacy Toys", "Funny Stacy" - 40 triệu sub, 22 triệu sub và 14,8 triệu sub

"Trùm cuối" của Top 5 này là cô bé Stacy với dòng máu lai cả Nga-Mỹ, còn có tên khác là Nastya theo tiếng Nga. Hiện Stacy đang sống cùng gia đình tại Florida (Mỹ) và sở hữu tận 3 kênh YouTube, tất cả đều có thành tích cực khủng.',
'tt1.jpg',1),
(N'Những ảnh hưởng tiêu cực của môi trường ô nhiễm, thiếu không gian xanh lên cuộc sống của con trẻ',
N'Ô nhiễm môi trường có thể gây ra các bệnh lý về đường hô hấp, gây nhức đầu, khó chịu khi đi học… Bên cạnh đó, việc biến những khu đất xanh thành các tòa nhà cao ốc cũng làm giảm diện tích khu vui chơi của trẻ em.
Chục năm trước, khi nói tới các vấn đề môi trường, người ta thường cảm thấy nó thuộc về phạm trù khá vĩ mô, với nào là hiệu ứng nhà kính, năng lượng khí thải, sự nóng lên của toàn cầu… Dường như, vấn đề này khi đó chỉ là câu chuyện xa xôi đặt trên bàn trị sự của các nguyên thủ quốc tế. Nhưng giờ đây, ô nhiễm môi trường đã trở thành một vấn đề cấp thiết và không thể ngó lơ. Bởi ảnh hưởng của nó đã len lỏi đến từng ngóc ngách, rúng nhiều hồi chuông cảnh báo về cả sức khỏe lẫn tinh thần con người. Con trẻ cũng bị ảnh hưởng là điều không tránh khỏi, bởi sức đề kháng của trẻ thường yếu lại có ít có khả năng chủ động đối phó với các yếu tố môi trường bên ngoài.',
'tt2.jpg',2),
(N'Những bức ảnh chứng minh trẻ thông minh hơn những gì người lớn tưởng',
N'Người lớn luôn cho rằng trẻ con thì biết gì đâu. Những bức ảnh dưới đây sẽ khiến bạn phải thay đổi suy nghĩ ngay lập tức.
Đó là khi trẻ phá vỡ những quy tắc, luật chơi mà bố mẹ đưa ra một cách dễ dàng mà bố mẹ không thể bắt bẻ gì. Đó là lúc trẻ đang hờn cả thế giới nhưng vẫn ngồi ăn tối mà không cần phải nhìn mặt mọi người. Là khi trẻ đánh dấu chủ quyền món ăn yêu thích theo cách riêng của mình. Hay như xem Ipad mà không cần dùng tay để cầm...
Mỗi bức ảnh là một bằng chứng cho thấy trẻ em rất thông minh, thậm chí còn thông minh hơn cả người lớn chúng ta.',
'tt3.jpg',2),
(N'Đi máy bay mà sợ ngồi gần những đứa trẻ thích “la làng la xóm”: Hãng hàng không Nhật Bản này đã tìm ra cách!'
,N'Nỗi ám ảnh bấy lâu nay của những hành khách thường xuyên di chuyển bằng máy bay giờ đã được một hãng hàng không ở Nhật Bản tiên phong tìm ra cách giải quyết. Tuy nhiên, câu chuyện này vẫn gây tranh cãi khá gay gắt trên cộng đồng mạng.
Đi máy bay giờ đây chắc chẳng còn là chuyện xa lạ đối với nhiều người, đặc biệt là các tín đồ du lịch trên khắp thế giới. Thế nhưng, trải nghiệm hàng không này cũng có lắm rắc rối và điều phiền toái. Một trong số đó chính là việc bị quấy rầy bởi những đứa trẻ con thích "làm ầm làm ĩ" và quấy phá khi ngồi máy bay.

"Nó còn con nít mà, có biết cái gì đâu" chắc hẳn là câu cửa miệng của một vài ông bà mẹ trong trường hợp này. Tuy nhiên, chỉ những ai từng trải qua mới hiểu được cảm giác khó chịu này.'
,'tt4.jpg',2),
(N'Khi trẻ em làm "giúp việc" cho nhà giàu: Bị ngược đãi tàn nhẫn, bữa ăn chan nước mắt và những cái chết đầy tức tưởi'
,N'Tuổi thơ của những đứa trẻ bị đánh cắp bởi cái nghèo đeo bám và sự vô cảm của những người lớn.
Mỗi tối, sau một ngày làm việc 12 tiếng đồng hồ, Neelum (11 tuổi) và Pari (13 tuổi) lại rời khỏi ngôi biệt thự có giá hàng triệu đô la, nơi các em đang làm giúp việc nhà tại đây, để trở về chỗ ở của các em. Nơi ở của hai đứa trẻ chỉ là những chiếc đệm mỏng, đã bị mối mọt và bữa ăn của các em là những thức ăn thừa của nhà chủ.

Ít ai ngờ rằng, đằng sau những cánh cửa kính lấp lánh trong khu nhà giàu bậc nhất ở Pakistan, hàng trăm trẻ em đang phải làm người hầu, giúp việc. Trên khắp đất nước Pakistan, ước tính có khoảng 264.000 trẻ em đang làm công việc này và đau lòng hơn, đa phần các em đều bị chủ nhân lạm dụng, ngược đãi.

Vào tháng 1 vừa qua, cả thế giới chấn động trước trường hợp của Uzma Bibi, 16 tuổi, làm giúp việc ở Lahore. Cô gái đã làm việc cho một cặp vợ chồng trong 9 tháng nhưng bị đối xử không bằng một con vật. Cô phải ngủ trên sàn nhà tắm lạnh lẽo, bị đánh đập liên tục và bị tra tấn bằng dây cáp điện.',
'tt5.jpg',3),
(N'Bé 11 tuổi sáng chế ra thiết bị phát hiện trẻ em bị bỏ quên trong xe, biết thổi hơi mát để câu giờ chờ người tới cứu'
,N'Sau khi xem mẩu tin đau lòng trên thời sự, cậu bé Bishop Curry đã quyết định hành động.
Truyền hình đưa tin dữ: một cô bé 6 tháng tuổi đã qua đời khi bị bỏ quên trong chiếc xe ô tô đặt dưới trời nắng. Cả nước Mỹ xót thương cho sinh linh bé bỏng, cậu bé Bishop Curry có vẻ shock hơn cả: cậu vẫn đi ngang qua ngôi nhà của cô bé xấu số mỗi lần tới trường.

Thế là cậu bé 11 tuổi quyết tâm giải quyết vấn đề nan giải, để không còn thấy tin dữ tương tự xuất hiện nữa. “Cháu đã nghĩ thế này, ‘Đây sẽ là cơ hội duy nhất để mình thực sự giúp đỡ được mọi người’”, Bishop nói với kênh tin tức NBC News.

Cậu bé phác thảo ra một thiết bị có khả năng cảm nhận thấy sự hiện của người trong xe. Nó sẽ được gắn lên ghế ngồi, phát tin nhắn cảnh báo tới cha mẹ hoặc cảnh sát khi nhận thấy có đứa trẻ bị bỏ quên trong xe, trong lúc đó, thiết bị sẽ thổi hơi mát để “câu giờ”.

Bishop Curry gọi thiết bị của mình là Oasis.',
'tt6.jpg',3),
(N'"Con yêu mẹ hay yêu bố hơn": Phần lớn trẻ sẽ chọn yêu bố, tại sao?',
N'Lũ trẻ đôi khi không nhận thức được những gì chúng đang nói.
Khi bạn hỏi một đứa trẻ dưới 3 tuổi: "Con yêu bố hay yêu mẹ hơn?". Có tới 64-85% cơ hội đứa trẻ đó sẽ trả lời là "Mẹ". Vậy thì có một mẹo nhỏ dành cho những ông bố, hãy đảo ngược lại câu hỏi: "Con yêu mẹ hơn hay yêu bố hơn?".

Bạn có thể đoán được câu trả lời chứ? Nghiên cứu cho thấy khi bạn đưa ra cho những đứa trẻ 2 sự lựa chọn, chúng sẽ thiên vị và chọn sự lựa chọn thứ hai, là từ được phát ra sau cùng và cũng là những gì chúng nghe thấy rõ ràng nhất.

Hiện tượng này được gọi là vòng lặp âm vị trong trí nhớ của trẻ. Ngay cả khi lựa chọn thứ hai là lựa chọn những đứa trẻ không thích, chúng vẫn có xu hướng chọn lựa chọn này hơn lựa chọn đầu tiên.

"Con thích ăn sô cô la hay ăn rau?", đó là một câu hỏi hiệu quả để giúp những đứa trẻ chọn ăn rau.'
,'tt7.jpg',3),
(N'Tạm giữ 4 trẻ em đánh cắp và tự lái xe ô tô đi cả nghìn km',
N'Ngày 15/7, giới chức Australia thông báo một vụ việc hy hữu khi cảnh sát nước này tạm giữ 4 trẻ em sau khi cả nhóm lái một chiếc xe bị đánh cắp khoảng 1.000 km dọc vùng hẻo lánh của Australia.
Nhóm "trộm nhí" gồm 3 cậu bé 13 và 14 tuổi cùng một cô bé 10 tuổi đã bắt đầu hành trình từ hôm 13/7 sau khi lấy trộm một chiếc xe của người thân cùng một số tiền mặt và cần câu cá từ thị trấn Rockhampton, bang Queensland. Một em trong số này đã để lại thư cho gia đình trình bày kế hoạch của nhóm.',
'tt8.jpg',4),
(N'Giáo viên mầm non bị sa thải vì phạt trẻ đứng ngoài trời nắng',
N'Theo South China morning post ngày 21/6, một giáo viên mầm non tư thục ở miền Đông Trung Quốc đã bị đuổi việc vì dùng hình phạt bắt 2 trẻ ở ngoài trời nắng.
Xuất hiện trong đoạn video ngắn đăng tải trên mạng xã hội, cậu bé đang nằm dưới sân, còn bé gái đang đứng ôm chiếc chăn. Hình ảnh trong video đã phải nhận chỉ trích dữ dội của cộng đồng mạng.

Minnan news, cổng thông tin điện tử một địa phương của tỉnh Phúc Kiến, cho biết sự việc xảy ra vào hôm thứ tư. Hai đứa trẻ bị bắt ra ngoài sân chơi vì làm ồn các bạn giờ ngủ trưa. Lúc này, nhiệt độ ngoài trời lên tới 33oC.',
'tt9.jpg',4),
(N'Phụ huynh nháo nhác tìm lớp, chi tiền triệu cho con ôn vào... lớp 1',
N'Còn gần 2 tháng nữa, năm học mới chính thức bắt đầu, nhưng thay vì vui chơi, giải trí, nhiều trẻ chuẩn bị vào lớp 1 lại đang phải học miệt mài.
Chị Nguyễn Hà Thy (Long Biên, Hà Nội) có con năm nay vào lớp 1 chia sẻ, đến thời điểm này, chị đã xác định được cho con đăng ký học trường công đúng tuyến, ngay gần nhà.

Năm học mới sẽ diễn ra vào tháng 8, nhưng ngay từ đầu tháng 5, chị Thy đã phải hỏi thăm khắp nơi, để tìm lớp học thêm tiền lớp 1 cho con. “Môi trường tiểu học khác hẳn với bậc mẫu giáo. Để con làm quen với những thứ mới như tập đọc, tập viết, tập trung học trong lớp nhiều giờ, tôi cho rằng việc học trước là cần thiết. Điều này sẽ giúp con không bị bỡ ngỡ khi vào lớp 1”.',
'tt10.jpg',4),
(N'Xe chở 40 phụ nữ và trẻ em lật nhào do nổ lốp, ít nhất 19 người tử vong',
N'Ngày 24/4, cảnh sát Nigeria thông báo ít nhất 19 người đã thiệt mạng và 21 người bị thương trong một vụ tai nạn xe khách nghiêm trọng tại bang Jigawa, miền Bắc nước này.
Theo nguồn tin cảnh sát, vụ tai nạn xảy ra vào ngày 23/4 tại làng Gwaram Sabuwa, cách thành phố Dutse, thủ phủ bang Jigawa, khoảng 100 km.

Chiếc xe khách 18 chỗ đã mất kiểm soát do bị nổ lốp, văng ra khỏi đường, lật nhào và bốc cháy.

Vào thời điểm xảy ra tai nạn, trên xe có 40 phụ nữ và trẻ em đang trở về nhà sau khi dự một đám cưới ở bang lân cận Bauchi.',
'tt11.jpg',5),
(N'Trẻ em Hàn Quốc thi nhau trang điểm, mỹ phẩm trở thành "đồ chơi thế hệ mới"',
N'Thị trường mỹ phẩm trẻ em ở Hàn Quốc đang phát triển vô cùng mạnh mẽ, những cô bé mẫu giáo còn chưa biết đọc rành rọt nhưng đã biết trang điểm kỹ lưỡng mỗi ngày.
Vẫn biết người dân Hàn Quốc vốn chú trọng đến việc làm đẹp, makeup, tuy nhiên không chỉ dừng lại ở đối tượng thanh niên, giờ đây đến trẻ mẫu giáo, tiểu học ở Hàn Quốc cũng thi nhau trang điểm, mỹ phẩm trở thành đồ chơi thế hệ mới.

Theo đó, tờ Washington Post mới đây vừa đăng tải bài viết về ngành công nghiệp mỹ phẩm dành cho trẻ em ở Hàn Quốc. Theo câu chuyện của cô nhóc mẫu giáo Yang Hye Ji trong bài, mỗi sáng đến trường, bên cạnh đồng phục hay sách vở, cô bé cũng chắc chắn luôn makeup kỹ lưỡng vì "Chúng sẽ khiến cháu trông xinh đẹp hơn".',
'tt12.jpg',5),
(N'Cuộc sống kinh hoàng tại thành phố ô nhiễm nhất thế giới: Bụi độc đến mức trẻ em phải ở yên trong nhà',
N'Vốn đã nổi danh là thủ đô lạnh nhất thế giới mà giờ đây, Ulaanbaatar còn thêm cái tiếng ô nhiễm nhất. Bụi than độc hại quyện hẳn một lớp dày, lên đến 345microgam/m3, gấp những 13,8 lần hàm lượng bụi mịn tiêu chuẩn (25mcg/m3/ngày) của Tổ chức Y tế Thế giới.
Nằm tại phía Bắc của miền Trung Mông Cổ, trên độ cao 1310m so với mặt nước biển, Ulaanbaatar vừa là trung tâm văn hóa, kinh tế vừa là thủ đô hành chính của quốc gia Mông Cổ.

Thành phố lạnh và ô nhiễm nhất

Tại Ulaanbaatar, mùa hè ấm áp vụt qua nhanh như gió thoảng còn mùa đông thì lỳ lợm kéo dài, mang băng giá và hanh khô trải khắp các ngả. Nó khiến cho nhiệt độ trung bình cả năm của thành phố xuống thấp tới mức -2,4C. Trong những thời điểm cực đoan, nhiệt độ còn xuống tới -40oC.',
'tt13.jpg',5)