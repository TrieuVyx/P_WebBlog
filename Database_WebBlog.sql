CREATE DATABASE blogDB
GO

USE blogDB
GO

CREATE TABLE __EFMigrationsHistory(
    MigrationId nvarchar(150) NOT NULL,
    ProductVersion nvarchar(32) NOT NULL,
    CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY CLUSTERED 
    (
        MigrationId ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE Account(
    AccountID int IDENTITY(1,1) NOT NULL,
    FullName nvarchar(max) NULL,
    Email nvarchar(max) NULL,
    Phone nvarchar(max) NULL,
    Password nvarchar(max) NULL,
    Salt nvarchar(10) NULL,
    Active bit NOT NULL,
    CreatedDate datetime NULL,
    LastLogin datetime NULL,
    RoleID int NULL,
    Address nvarchar(max) NULL,
    ProfileImage nvarchar(max) NULL,
    CONSTRAINT PK_Accounts PRIMARY KEY CLUSTERED 
    (
        AccountID ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE Category(
    CategoryID int IDENTITY(1,1) NOT NULL,
    CategoryName nvarchar(max) NULL,
    Title nvarchar(max) NULL,
    Alias nvarchar(max) NULL,
    MetaDesc nvarchar(max) NULL,
    MetaKey nvarchar(max) NULL,
    Thumb nvarchar(max) NULL,
    Icon nvarchar(max) NULL,
    Cover nvarchar(max) NULL,
    Description nvarchar(max) NULL,
    Published bit NOT NULL,
    Ordering int NULL,
    Parents int NULL,
    Levels int NULL,
    CONSTRAINT PK_Category PRIMARY KEY CLUSTERED 
    (
        CategoryID ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE Posts (
    PostID INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(MAX),
    ShortContents NVARCHAR(MAX),
    Contents NVARCHAR(MAX),
    Thumb NVARCHAR(MAX),
    Alias NVARCHAR(MAX),
    Author NVARCHAR(MAX),
    Tags NVARCHAR(MAX),
    CategoryID INT,
    Published BIT NOT NULL,
    isHot BIT NOT NULL,
    AccountID INT,
    isNewFeed BIT NOT NULL,
    CreatedDate DATETIME,
    Views INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Posts PRIMARY KEY CLUSTERED 
    (
        PostID ASC
    ),
    CONSTRAINT FK_Posts_Categories FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    CONSTRAINT FK_Posts_Accounts FOREIGN KEY (AccountID) REFERENCES Account(AccountID)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(MAX) NULL,
    RoleDescription NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED 
    (
        RoleID ASC
    )
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE Account WITH CHECK ADD CONSTRAINT FK_Account_Roles FOREIGN KEY(RoleID)
REFERENCES Roles(RoleID)
GO

ALTER TABLE Account CHECK CONSTRAINT FK_Account_Roles
GO

INSERT INTO Roles	(RoleName,	RoleDescription)
VALUES				(N'Admin',	N'Quản Trị Hệ Thống'),
					(N'User',	N'Người Dùng Thông Thường')

INSERT INTO Category(CategoryName,	Title,				Alias,		MetaDesc,					MetaKey,				Thumb,				Icon,				Cover,				Description,					Published,	Ordering,	Parents,	Levels)
VALUES				(N'Tech',		N'Tech Category',	N'Tech',	N'Tech related articles',	N'tech, technology',	N'tech_thumb.jpg',	N'tech_icon.jpg',	N'tech_cover.jpg',	N'Tech category description',	1,			1,			NULL,		NULL),
					(N'Food',		N'Food Category',	N'Food',	N'Food related articles',	N'food, cuisine',		N'food_thumb.jpg',	N'food_icon.jpg',	N'food_cover.jpg',	N'Food category description',	1,			2,			NULL,		NULL),
					(N'Sport',		N'Sport Category',	N'Sport',	N'Sport related articles',	N'sport, athletics',	N'sport_thumb.jpg', N'sport_icon.jpg',	N'sport_cover.jpg', N'Sport category description',	1,			3,			NULL,		NULL);

-- Khai báo biến để lưu trữ thông tin tài khoản
DECLARE @FullName nvarchar(max) = 'John Doe';
DECLARE @FullName1 nvarchar(max) = 'Doe smith';
DECLARE @Email nvarchar(max) = 'admin1@gmail.com';
DECLARE @Email1 nvarchar(max) = 'user1@gmail.com';
DECLARE @Phone nvarchar(max) = '123456789';
DECLARE @Phone1 nvarchar(max) = '123456788';
DECLARE @Salt nvarchar(10) = 'somesaltvalue';
DECLARE @Active bit = 0;
DECLARE @CreatedDate datetime = GETDATE();
DECLARE @LastLogin datetime = NULL; -- Có thể thay đổi ngày giờ tùy theo yêu cầu
DECLARE @RoleID int = 1; -- ID của vai trò
DECLARE @RoleID1 int = 2; -- ID của vai trò
DECLARE @Address nvarchar(max) = '123 Main St, City';
DECLARE @Address1 nvarchar(max) = '128 Main St, City';
DECLARE @ProfileImage nvarchar(max) = 'https://ik.imagekit.io/alejk5lwty/image/3408a6f11daf512b6e5d1cadd72b83be.jpg?updatedAt=1678593086796';
DECLARE @ProfileImage1 nvarchar(max) = 'https://ik.imagekit.io/alejk5lwty/image/3408a6f11daf512b6e5d1cadd72b83be.jpg?updatedAt=1678593086796';
-- Chèn dữ liệu vào bảng Account
INSERT INTO Account (
    FullName,	Email,		Phone,	Password,												Active,		CreatedDate,	LastLogin,		RoleID,		Address,	ProfileImage
)
VALUES 
(
    @FullName,	@Email,		@Phone, CONVERT(nvarchar(max), HASHBYTES('MD5', '123'), 2),		@Active,	@CreatedDate,	@LastLogin,		@RoleID,	@Address,	@ProfileImage
),

(
    @FullName1,	@Email1,	@Phone1, CONVERT(nvarchar(max), HASHBYTES('MD5', '123'), 2),	@Active,	@CreatedDate,	@LastLogin,		@RoleID1,	@Address1,	@ProfileImage1
);




INSERT INTO Posts	(Title,													ShortContents,																											Contents,																																																																																																																														Thumb,																											Alias,													Author,				Tags,									CategoryID, Published,	isHot,	AccountID,	isNewFeed,	CreatedDate,	Views)
VALUES 
					(N'Công nghệ mới trong ngành công nghiệp',				N'Bài viết này giới thiệu về những công nghệ mới nhất trong ngành công nghiệp.',										N'Công nghệ ngày càng đóng vai trò quan trọng trong các ngành công nghiệp hiện đại. Điều này đòi hỏi các doanh nghiệp phải cập nhật và áp dụng các công nghệ mới nhằm nâng cao năng suất, tăng cường độ tin cậy và tối ưu hóa quy trình sản xuất. Bài viết này sẽ giới thiệu về những công nghệ đột phá như trí tuệ nhân tạo, Internet of Things (IoT), máy học và tự động hóa trong ngành công nghiệp. Chúng ta sẽ tìm hiểu về các ứng dụng và tiềm năng của công nghệ trong việc gia tăng năng suất và hiệu quả sản xuất.',	N'https://ik.imagekit.io/alejk5lwty/Image-food/c1f023b2ad9585b8caaf19033222eae5.jpg?updatedAt=1693392070299',	N'cong-nghe-moi-trong-cong-nghiep',						N'Nguyễn Văn A',	N'tech, công nghệ, công nghiệp',		1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách làm món bánh chocolate ngon',					N'Bài viết này chia sẻ một công thức đơn giản để làm món bánh chocolate thơm ngon.',									N'Bánh chocolate là một món tráng miệng hấp dẫn và phổ biến. Nếu bạn muốn tự tay làm bánh chocolate thì đây là công thức hoàn hảo cho bạn. Chúng ta sẽ tìm hiểu cách chuẩn bị nguyên liệu và các bước làm bánh từ đơn giản như trộn bột, nướng bánh cho đến phức tạp hơn như làm sốt chocolate và trang trí bánh. Bài viết cũng cung cấp những lời khuyên và bí quyết để bánh của bạn trở nên thơm ngon và hấp dẫn.',																											N'https://ik.imagekit.io/alejk5lwty/Image-food/90bcaf8a2477cb6e03d1a6e6401bca9f.jpg?updatedAt=1693391080771',	N'cach-lam-mon-banh-chocolate-ngon',					N'Nguyễn Thị B',	N'food, bánh, chocolate',				2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách chọn smartphone phù hợp cho nhu cầu cá nhân',	N'Bài viết này cung cấp hướng dẫn cho việc chọn một chiếc smartphone phù hợp với nhu cầu cá nhân của bạn.',				N'Trên thị trường hiện nay có rất nhiều lựa chọn về smartphone, và việc chọn một chiếc phù hợp có thể khá khó khăn. Bài viết này sẽ giới thiệu về những yếu tố cần xem xét khi chọn smartphone như hệ điều hành, camera, hiệu năng và kích thước màn hình. Chúng ta cũng sẽ tìm hiểu về các công nghệ mới như 5G và trí tuệ nhân tạo trong smartphone.',																																										N'https://ik.imagekit.io/alejk5lwty/Image-food/9f23015437cae9e475d30fc2753c7ee8.jpg?updatedAt=1693392169964',	N'cach-chon-smartphone-phu-hop-cho-nhu-cau-ca-nhan',	N'Lê Thị E',		N'tech, smartphone, chọn mua',			1,			1,			1,		1,			1,			GETDATE(),		0),    
					(N'Mẹo nấu ăn tiết kiệm thời gian cho người bận rộn',	N'Bài viết này chia sẻ những mẹo nấu ăn giúp tiết kiệm thời gian đối với những người có lịch trình bận rộn.',			N'Với cuộc sống hiện đại, thời gian trở thành một nguồn tài nguyên quý giá. Bài viết này sẽ cung cấp những mẹo nấu ăn nhanh và tiết kiệm thời gian như sử dụng nồi áp suất, chuẩn bị nguyên liệu trước và áp dụng kỹ thuật nấu ăn tiện lợi. Chúng ta cũng sẽ tìm hiểu về các công cụ và thiết bị hỗ trợ giúp bạn nấu ăn hiệu quả.',																																																N'https://ik.imagekit.io/alejk5lwty/Image-food/57261a3cefc3665cc13cc5d94343785c.jpg?updatedAt=1693392270080',	N'meo-nau-an-tiet-kiem-thoi-gian-cho-nguoi-ban-ron',	N'Nguyễn Văn F',	N'food, cooking, tiết kiệm thời gian',	2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Các bài tập cơ bản để rèn luyện sức mạnh',			N'Bài viết này giới thiệu về các bài tập cơ bản giúp bạn rèn luyện sức mạnh cơ bắp.',									N'Sức mạnh cơ bắp không chỉ quan trọng trong thể thao mà còn đóng vai trò quan trọng trong cuộc sống hàng ngày. Bài viết này sẽ giới thiệu về các bài tập cơ bản như squat, push-up, deadlift và bench press. Chúng ta cũng sẽ tìm hiểu về cách thực hiện đúng các bài tập và tần suất luyện tập phù hợp.',																																																						N'https://ik.imagekit.io/alejk5lwty/Image-food/21d4d874cffcba54c2b9d219103d3f3a.jpg?updatedAt=1693391080837',	N'cac-bai-tap-co-ban-de-ren-luyen-suc-manh',			N'Trần Thị G',		N'sport, rèn luyện, sức mạnh',			3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách tăng tốc độ Internet của bạn',					N'Bài viết này cung cấp những gợi ý và phương pháp để tăng tốc độ kết nối Internet của bạn.',							N'Khi Internet chậm, nó có thể gây khó chịu và ảnh hưởng đến trải nghiệm trực tuyến của bạn. Bài viết này sẽ giới thiệu về các cách tăng tốc độ Internet như cập nhật firmware, sử dụng bộ khuếch đại tín hiệu, và kiểm tra và tối ưu hóa tốc độ kết nối của bạn. Chúng ta cũng sẽ tìm hiểu về các yếu tố ảnh hưởng đến tốc độ Internet và cách khắc phục các vấn đề phổ biến.',																																				N'https://ik.imagekit.io/alejk5lwty/Image-food/60064b78f8dea990633c05f8a16a4eef.jpg?updatedAt=1693392324262',	N'cach-tang-toc-do-internet-cua-ban',					N'Ngô Văn H',		N'tech, internet, tăng tốc độ',			1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách nấu món bò bít tết ngon tại nhà',				N'Bài viết này chia sẻ công thức và bước làm món bò bít tết ngon tại nhà.',												N'Bò bít tết là một món ăn phổ biến và ngon miệng. Bài viết này sẽ cung cấp hướng dẫn chi tiết về cách nấu món bò bít tết tại nhà, từ việc chọn loại thịt phù hợp, chuẩn bị gia vị, cho đến quá trình nấu nướng và trình bày món ăn. Chúng ta cũng sẽ tìm hiểu về các phụ kiện và sốt thích hợp để kèm theo bò bít tết.',																																																		N'https://ik.imagekit.io/alejk5lwty/Image-food/1e2830d5fa12064132a47b9e2f860f74.jpg?updatedAt=1693391080801',	N'cach-nau-mon-bo-bit-tet-ngon-tai-nha',				N'Trần Thị I',		N'food, cooking, bò bít tết',			2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Các bài tập cardio giúp đốt cháy mỡ thừa',			N'Bài viết này giới thiệu về các bài tập cardio giúp đốt cháy mỡ thừa và tăng cường sức khỏe tim mạch.',				N'Bài tập cardio là một phương pháp hiệu quả để giảm cân và cải thiện sức khỏe tim mạch. Bài viết này sẽ giới thiệu về các bài tập cardio như chạy bộ, bơi lội, và nhảy dây. Chúng ta cũng sẽ tìm hiểu về cách lập kế hoạch tập luyện cardio và các lợi ích của việc thực hiện các bài tập này.',																																																								N'https://ik.imagekit.io/alejk5lwty/Image-food/e515fb03f2330a588796247f466d92f7.jpg?updatedAt=1693391080824',	N'cac-bai-tap-cardio-giup-dot-chay-mo-thua',			N'Phạm Văn J',		N'sport, cardio, giảm cân',				3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách làm món sushi tại nhà',							N'Bài viết này chia sẻ các bước chi tiết để bạn có thể làm món sushi ngon tại nhà.',									N'Bài viết này chia sẻ các bước chi tiết để bạn có thể làm món sushi ngon tại nhà. Từ cách chọn nguyên liệu, chuẩn bị cơ bản đến kỹ thuật cuộn sushi và các mẹo nhỏ để món sushi của bạn trở nên hoàn hảo. Bạn sẽ được hướng dẫn từ việc nấu cơm sushi, cách chế biến các loại hải sản và rau sống, cũng như cách cuộn sushi thành các loại hình khác nhau. Bài viết cũng cung cấp những gợi ý về các loại sốt và gia vị phù hợp để tăng thêm hương vị cho món sushi.',															N'https://ik.imagekit.io/alejk5lwty/Image-food/d4499f9045e483d75411cf756e46ddf8.jpg?updatedAt=1693392427524',	N'cach-lam-mon-sushi-tai-nha',							N'Nguyễn Văn A',	N'food, sushi',							2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Công nghệ Blockchain và ứng dụng trong cuộc sống',	N'Bài viết này giới thiệu về công nghệ Blockchain và những ứng dụng tiềm năng của nó trong cuộc sống hàng ngày.',		N'Bài viết này giới thiệu về công nghệ Blockchain và những ứng dụng tiềm năng của nó trong cuộc sống hàng ngày. Bạn sẽ được tìm hiểu về cơ bản của công nghệ Blockchain, cách hoạt động và lợi ích mà nó mang lại. Bài viết cũng đề cập đến các lĩnh vực ứng dụng của Blockchain như tài chính, chuỗi cung ứng, quản lý dữ liệu và quyền riêng tư. Bên cạnh đó, chúng ta cũng sẽ thảo luận về thách thức và tiềm năng phát triển của công nghệ này.',																			N'https://ik.imagekit.io/alejk5lwty/Image-food/333466cd07d18eae8c6546e298f2b9c7.jpg?updatedAt=1693391080877',	N'cong-nghe-blockchain-va-ung-dung-trong-cuoc-song',	N'Nguyễn Thị B',	N'tech, blockchain',					1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách nâng cao kỹ thuật đá banh',						N'Bài viết này cung cấp một số gợi ý và bài tập để nâng cao kỹ thuật đá banh của bạn.',									N'Từ cách kiểm soát bóng, chuyền bóng, đá phạt đền đến kỹ thuật sút bóng và đánh đầu, bạn sẽ được hướng dẫn chi tiết về cách cải thiện các kỹ năng cơ bản và phát triển kỹ thuật đá banh chuyên nghiệp.',																																																																														N'https://ik.imagekit.io/alejk5lwty/Image-food/e79753af072d6fc80f17efc56244645c.jpg?updatedAt=1693391080855',	N'cach-nang-cao-ky-thuat-da-banh',						N'Nguyễn Văn A',	N'sport, đá banh',						3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách làm món bánh mì nướng thơm ngon',				N'Bài viết này chia sẻ công thức và hướng dẫn cách làm món bánh mì nướng thơm ngon tại nhà.',							N'Bạn sẽ được hướng dẫn từ việc chuẩn bị nguyên liệu, làm bột, cho men bột, và phương pháp nướng bánh mì để đạt được kết quả tuyệt vời. Bài viết cũng chia sẻ một số gợi ý về nhân và topping phổ biến cho bánh mì nướng.',																																																																										N'https://ik.imagekit.io/alejk5lwty/Image-food/113035fa03eeffe4ea3d36206fe64924.jpg?updatedAt=1693391712302',	N'cach-lam-mon-banh-mi-nuong-thom-ngon',				N'Trần Văn C',		N'food, bánh mì',						2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách bảo mật dữ liệu trực tuyến',					N'Bài viết này cung cấp một số gợi ý và phương pháp để bảo mật dữ liệu trực tuyến của bạn.',							N'Bạn sẽ được tìm hiểu về các biện pháp bảo mật quan trọng như sử dụng mật khẩu mạnh, xác thực hai yếu tố, mã hóa dữ liệu và cập nhật phần mềm. Bài viết cũng đề cập đến các lỗ hổng bảo mật phổ biến và cách tránh chúng.',																																																																									N'https://ik.imagekit.io/alejk5lwty/Image-food/9d59e1a23811e940668856e04a91c06d.jpg?updatedAt=1693391080878',	N'cach-bao-mat-du-lieu-truc-tuyen',						N'Lê Thị D',		N'tech, bảo mật',						1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Bí quyết luyện tập thể lực cho người chơi bóng đá',	N'Bài viết này chia sẻ bí quyết và phương pháp luyện tập thể lực hiệu quả cho người chơi bóng đá.',						N'Bạn sẽ được tìm hiểu về các bài tập cardio, tăng cường sức mạnh, nâng cao sự linh hoạt và cải thiện sự bền bỉ. Bài viết cũng đề cập đến một số nguyên tắc dinh dưỡng cần lưu ý để duy trì sức khỏe và hiệu suất tốt trong quá trình luyện tập.',																																																																				N'https://ik.imagekit.io/alejk5lwty/Image-food/715c40d7fb1fb91da7c341082822786d.jpg?updatedAt=1693391080652',	N'bi-quyet-luyen-tap-the-luc-cho-nguoi-choi-bong-da',	N'Phạm Văn E',		N'sport, bóng đá',						3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Công nghệ trí tuệ nhân tạo và tương lai',			N'Bài viết này giới thiệu về công nghệ trí tuệ nhân tạo (AI) và triển vọng của nó trong tương lai.',					N'Bạn sẽ được tìm hiểu về khái niệm và ứng dụng của trí tuệ nhân tạo, cũng như những tiềm năng và thách thức mà AI mang lại. Bài viết cũng đề cập đến các lĩnh vực ứng dụng của trí tuệ nhân tạo như xe tự lái, chăm sóc sức khỏe và dịch vụ khách hàng.',																																																																		N'https://ik.imagekit.io/alejk5lwty/Image-food/682ab50a9a3993d852bc7dbee0c9cf54.jpg?updatedAt=1693391941006',	N'cong-nghe-tri-tue-nhan-tao-va-tuong-lai',				N'Lê Văn G'	,		N'tech, trí tuệ nhân tạo',				1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách chọn và chăm sóc giày chạy bộ',					N'Bài viết này cung cấp hướng dẫn cách chọn và chăm sóc giày chạy bộ để đảm bảo thoải mái và an toàn khi tập luyện.',	N'Bạn sẽ được tìm hiểu về các yếu tố quan trọng khi chọn giày chạy bộ như loại hình tập luyện, kiểu dáng và kích thước. Bài viết cũng chia sẻ một số phương pháp chăm sóc giày chạy bộ để gia tăng tuổi thọ và hiệu suất sử dụng.',																																																																								N'https://ik.imagekit.io/alejk5lwty/Image-food/63de23b7ad0cb2e2438c5e6b741ac758.jpg?updatedAt=1693391080639',	N'cach-chon-va-cham-soc-giay-chay-bo',					N'Phạm Thị H',		N'sport, giày chạy bộ',					3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách nấu món gà kho gừng thơm ngon',					N'Bài viết này chia sẻ công thức và hướng dẫn cách nấu món gà kho gừng thơm ngon tại nhà.',								N'Bạn sẽ được hướng dẫn từ việc chuẩn bị nguyên liệu, cách chế biến gà, và phương pháp kho để tạo ra món gà kho gừng thơm ngon. Bài viết cũng chia sẻ một số mẹo nhỏ để làm món gà kho thêm phần hấp dẫn.',																																																																														N'https://ik.imagekit.io/alejk5lwty/Image-food/d363db750b9aa812f1730e72d925df72.jpg?updatedAt=1693391086884',	N'cach-nau-mon-ga-kho-gung-thom-ngon',					N'Nguyễn Văn I',	N'food, gà kho',						2,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Internet of Things (IoT) và cuộc cách mạng kết nối', N'Bài viết này giới thiệu về công nghệ Internet of Things (IoT) và tầm quan trọng của nó trong cuộc sống hiện đại.',	N'Internet of Things (IoT) là một mạng lưới các thiết bị được kết nối với nhau và có khả năng trao đổi thông tin. Bài viết sẽ trình bày về các ứng dụng của IoT trong các lĩnh vực như công nghiệp, y tế, giao thông và gia đình thông minh. Ngoài ra, bài viết cũng sẽ đề cập đến các vấn đề về bảo mật và quyền riêng tư trong môi trường IoT.',																																												N'https://ik.imagekit.io/alejk5lwty/Image-food/9d59e1a23811e940668856e04a91c06d.jpg?updatedAt=1693391080878',	N'internet-of-things-va-cuoc-cach-mang-ket-noi',		N'Trần Thị B',		N'tech, IoT',							1,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Các kỹ thuật quan trọng trong bóng đá',				N'Bài viết này giới thiệu về các kỹ thuật quan trọng trong bóng đá.',													N'Bóng đá là môn thể thao được yêu thích trên toàn thế giới, và để thành công trong bóng đá, cầu thủ cần phải nắm vững các kỹ thuật cơ bản. Bài viết này sẽ giới thiệu về các kỹ thuật quan trọng như chuyền bóng, dứt điểm, phòng ngự và điều chỉnh tấn công. Chúng ta sẽ tìm hiểu về cách thực hiện các kỹ thuật này, cùng những lời khuyên để cải thiện kỹ năng và hiểu rõ hơn về cách áp dụng chúng trong trận đấu thực tế.',																								N'https://ik.imagekit.io/alejk5lwty/Image-food/97c8b7f9bcc049d9333a9aa411afebaf.jpg?updatedAt=1693391080751',	N'cac-ky-thuat-quan-trong-trong-bong-da',				N'Trần Văn C',		N'sport, bóng đá, kỹ thuật',			3,			1,			1,		1,			1,			GETDATE(),		0),
					(N'Cách rèn luyện thể lực cho cầu thủ bóng đá',			N'Bài viết này chia sẻ các phương pháp rèn luyện thể lực hiệu quả cho cầu thủ bóng đá.',								N'Thể lực đóng vai trò quan trọng trong bóng đá, và việc rèn luyện thể lực đúng cách có thể giúp cầu thủ cải thiện hiệu suất thi đấu và tránh chấn thương. Bài viết này sẽ giới thiệu về các phương pháp rèn luyện thể lực như tập cardio, tăng cường sức mạnh, và nâng cao sự linh hoạt. Chúng ta cũng sẽ tìm hiểu về chế độ ăn uống và giấc ngủ phù hợp để cung cấp năng lượng và phục hồi cơ bắp cho cầu thủ.',																												N'https://ik.imagekit.io/alejk5lwty/Image-food/06b91fb3986651c61fa6fe936e92dc73.jpg?updatedAt=1693392560584',	N'cach-ren-luyen-the-luc-cho-cau-thu-bong-da',			N'Phạm Thị D',		N'sport, bóng đá, thể lực',				3,			1,			1,		1,			1,			GETDATE(),		0);

