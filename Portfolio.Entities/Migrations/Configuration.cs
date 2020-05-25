namespace Portfolio.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Portfolio.Entities.PortfolioContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Portfolio.Entities.PortfolioContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ImageUseTypes.AddOrUpdate(new Models.ImageUseType { ImageUseTypeId = 1, TypeName = "메인 섬네일", UseYn = true });
            context.ImageUseTypes.AddOrUpdate(new Models.ImageUseType { ImageUseTypeId = 2, TypeName = "서브 섬네일", UseYn = true });
            context.ImageUseTypes.AddOrUpdate(new Models.ImageUseType { ImageUseTypeId = 3, TypeName = "상품 세부 이미지", UseYn = true });
            context.ImageUseTypes.AddOrUpdate(new Models.ImageUseType { ImageUseTypeId = 4, TypeName = "상품 문의 이미지", UseYn = true });
            context.ImageUseTypes.AddOrUpdate(new Models.ImageUseType { ImageUseTypeId = 5, TypeName = "상품 후기 이미지", UseYn = true });

            context.Categories.AddOrUpdate(new Models.Category { CategoryId = 1, CategoryName = "전략", Order = 1 });
            context.Categories.AddOrUpdate(new Models.Category { CategoryId = 2, CategoryName = "가족", Order = 2 });
            context.Categories.AddOrUpdate(new Models.Category { CategoryId = 3, CategoryName = "파티", Order = 3 });
            context.Categories.AddOrUpdate(new Models.Category { CategoryId = 4, CategoryName = "테마", Order = 4 });

            context.Products.AddOrUpdate(new Models.Product { ProductId = 1, CategoryId = 3, ProductName = "달무티", Summary = "한글판,8세이상,4-8인용", Price = 12000, PromotionPrice = 7290 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 2, CategoryId = 1, ProductName = "테라포밍마스", Summary = "14세이상,1~5인용,90분이상", Price = 62000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 3, CategoryId = 1, ProductName = "테라포밍마스 확장 헬라스앤엘리시움", Summary = "만14세이상,1~5인용,90~120분", Price = 18000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 4, CategoryId = 1, ProductName = "테라포밍마스 확장 서곡", Summary = "플레이타임을 줄여주는 추천확장", Price = 22000, PromotionPrice = 14200 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 5, CategoryId = 1, ProductName = "브라스 버밍엄", Summary = "2~4명,경제게임의 걸작", Price = 89000, PromotionPrice = 66750 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 6, CategoryId = 2, ProductName = "스플렌더", Summary = "2~4명, 2014년 SDJ 후보작", Price = 49000, PromotionPrice = 34300 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 7, CategoryId = 2, ProductName = "스플렌더 확장 찬란한 도시", Summary = "2~4명, 스플렌더 확장팩", Price = 49000, PromotionPrice = 34300 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 8, CategoryId = 1, ProductName = "아그리콜라", Summary = "1~4명, 일꾼 놓기의 명작", Price = 66000, PromotionPrice = 43660 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 9, CategoryId = 1, ProductName = "아그리콜라 패밀리", Summary = "1~4명, 가족 농장 경영, 좀 더 쉽게", Price = 48000, PromotionPrice = 32600 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 10, CategoryId = 1, ProductName = "도미니언", Summary = "최고의 중독성 덱빌딩 카드게임", Price = 54000, PromotionPrice = 37800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 11, CategoryId = 1, ProductName = "도미니언: 장막 뒤의 사람들", Summary = "더욱 더 전략적이고 공격적인 확장팩", Price = 55000, PromotionPrice = 38500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 12, CategoryId = 1, ProductName = "테라 미스티카 - 빅박스", Summary = "판타지 종족들의 치열한 땅따먹기", Price = 110000, PromotionPrice = 88000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 13, CategoryId = 1, ProductName = "티츄 디럭스: 레드", Summary = "더 컴팩트하고 풍부해진 티츄", Price = 12000, PromotionPrice = 8000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 14, CategoryId = 1, ProductName = "티츄 디럭스: 블루", Summary = "더 컴팩트하고 풍부해진 티츄", Price = 12000, PromotionPrice = 8000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 15, CategoryId = 1, ProductName = "7 원더스", Summary = "2010년 최고의 화제 게임, 문명 게임의 기린아", Price = 62000, PromotionPrice = 43400 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 16, CategoryId = 1, ProductName = "파워그리드", Summary = "경제/건설 게임의 최고봉", Price = 52000, PromotionPrice = 35000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 17, CategoryId = 1, ProductName = "시즌스", Summary = "시디트 왕국의 대마법사가 되어보세요", Price = 59000, PromotionPrice = 45000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 18, CategoryId = 1, ProductName = "에이언즈 엔드", Summary = "네메시스를 물리치는 협력게임", Price = 119000, PromotionPrice = 85000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 19, CategoryId = 1, ProductName = "팬데믹", Summary = "세계적인 협력게임 붐을 불러일으킨 대작", Price = 49000, PromotionPrice = 34500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 20, CategoryId = 1, ProductName = "팬데믹 레거시:레드", Summary = "질병처럼 전세계를 강타한 역작 첫번째 이야기", Price = 75000, PromotionPrice = 58500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 21, CategoryId = 1, ProductName = "푸에르토 리코", Summary = "10년이 지나도 영원한 명작", Price = 39900, PromotionPrice = 35000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 22, CategoryId = 1, ProductName = "푸에르토 리코: 카드게임", Summary = "카드게임으로 즐기는 명작", Price = 25000, PromotionPrice = 21500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 23, CategoryId = 1, ProductName = "안드로이드 넷러너", Summary = "최고의 2인용 전략게임", Price = 49000, PromotionPrice = 34500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 24, CategoryId = 1, ProductName = "브라스 - 랭커셔", Summary = "경제 게임의 걸작", Price = 89000, PromotionPrice = 66750 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 25, CategoryId = 1, ProductName = "요코하마", Summary = "다양하고 풍성한 전략의 일꾼 놓기 게임", Price = 67000, PromotionPrice = 47000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 26, CategoryId = 1, ProductName = "사이쓰", Summary = "1-5명, 자신의 제국을 건설하고 부와 명예를 누려라", Price = 90000, PromotionPrice = 69000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 27, CategoryId = 1, ProductName = "사이쓰 확장 먼곳에서온침략자", Summary = "명작 사이쓰 첫번째 확장", Price = 40000, PromotionPrice = 29900 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 28, CategoryId = 2, ProductName = "스컬킹", Summary = "대히트 트릭 테이킹 게임", Price = 23000, PromotionPrice = 15000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 29, CategoryId = 2, ProductName = "라스베가스", Summary = "2012 알레아. 정말로 쉬운 게임을 선보이다", Price = 29000, PromotionPrice = 26000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 30, CategoryId = 2, ProductName = "젝스님트", Summary = "멘사셀렉트 최고의 두뇌 게임", Price = 12000, PromotionPrice = 8000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 31, CategoryId = 2, ProductName = "보난자", Summary = "콩심은데 돈난다", Price = 14000, PromotionPrice = 9800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 32, CategoryId = 2, ProductName = "티켓 투 라이드 - 유럽", Summary = "티켓 투 라이드 최고 인기버전", Price = 69000, PromotionPrice = 45000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 33, CategoryId = 2, ProductName = "카탄", Summary = "전략 보드게임의 첫 걸음. 카탄의 개척자", Price = 49000, PromotionPrice = 34500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 34, CategoryId = 2, ProductName = "카르카손", Summary = "최고의 타일 놓기 게임", Price = 33000, PromotionPrice = 23000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 35, CategoryId = 2, ProductName = "차이나타운", Summary = "협상을 통해 최고의 상인이 되세요", Price = 55000, PromotionPrice = 42000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 36, CategoryId = 2, ProductName = "딕싯", Summary = "모두가 쉽게 즐길 수 있는 진정한 감성/파티게임", Price = 44000, PromotionPrice = 30800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 37, CategoryId = 2, ProductName = "브룸 서비스", Summary = "[마녀의 물약]이 새롭게 리메이크 되었어요", Price = 41000, PromotionPrice = 37000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 38, CategoryId = 2, ProductName = "러브 레터", Summary = "최고의 베스트 셀러 게임 러브레터 2017에디션", Price = 15000, PromotionPrice = 9600 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 39, CategoryId = 2, ProductName = "마닐라", Summary = "베스트 셀러 가족게임", Price = 49000, PromotionPrice = 33000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 40, CategoryId = 2, ProductName = "패치워크", Summary = "정말로 쉽게 즐길 수 있는 2인용 게임", Price = 22000, PromotionPrice = 15000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 41, CategoryId = 2, ProductName = "루미큐브", Summary = "사고력과 계산력을 향상 시키는 최고의 게임", Price = 36000, PromotionPrice = 24000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 42, CategoryId = 2, ProductName = "우봉고", Summary = "본격 퍼즐 게임 프랜차이즈의 오리지널", Price = 49000, PromotionPrice = 33000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 43, CategoryId = 2, ProductName = "우봉고 3D", Summary = "베스트 셀러 우봉공의 3D버전", Price = 89000, PromotionPrice = 71000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 44, CategoryId = 2, ProductName = "카멜업", Summary = "2014년 독일 올해의 게임상 수상작", Price = 39000, PromotionPrice = 27000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 45, CategoryId = 2, ProductName = "더 마인드", Summary = "소리없이 진행되는 협력 게임", Price = 15000, PromotionPrice = 13100 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 46, CategoryId = 3, ProductName = "사보타지", Summary = "대 히트 게임 사보타지 컴백", Price = 12000, PromotionPrice = 8400 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 47, CategoryId = 3, ProductName = "노 땡스", Summary = "언제 어디서나 재미만점", Price = 12000, PromotionPrice = 8400 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 48, CategoryId = 3, ProductName = "할리갈리 디럭스", Summary = "웃음과 재미로 가득찬 최고의 파티게임", Price = 22000, PromotionPrice = 15400 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 49, CategoryId = 3, ProductName = "시타델", Summary = "협상과 협잡이 난무하는 권모술수", Price = 30000, PromotionPrice = 21000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 50, CategoryId = 3, ProductName = "텔레스트레이션", Summary = "그리면서 즐기는 텔레파시 게임", Price = 39000, PromotionPrice = 27300 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 51, CategoryId = 3, ProductName = "한밤의 늑대인간", Summary = "마피아 게임의 신 개념", Price = 29000, PromotionPrice = 21500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 52, CategoryId = 3, ProductName = "뱅!", Summary = "더 이상의 설명이 필요없는 베스트 셀러 마피아 게임", Price = 28000, PromotionPrice = 20000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 53, CategoryId = 3, ProductName = "뱅! 주사위 게임", Summary = "뱅!이 주사위 게임으로 돌아왔다", Price = 25000, PromotionPrice = 17000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 54, CategoryId = 3, ProductName = "너도? 나도!", Summary = "이심전심 공감게임! 너도? 나도!", Price = 25000, PromotionPrice = 16250 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 55, CategoryId = 3, ProductName = "한밤의 수수께끼", Summary = "늑대인간을 쫓아낼 마법의 단어를 찾아라", Price = 28000, PromotionPrice = 22000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 56, CategoryId = 3, ProductName = "황혼에서 새벽까지", Summary = "한밤의 늑대인간2", Price = 28000, PromotionPrice = 22000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 57, CategoryId = 3, ProductName = "바퀴벌레 포커 로얄", Summary = "독일 가족 게임 베스트 셀러의 업그레이드 버전", Price = 16000, PromotionPrice = 12000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 58, CategoryId = 3, ProductName = "포 세일", Summary = "최고의 투자는 무엇일까요?", Price = 27500, PromotionPrice = 22000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 59, CategoryId = 3, ProductName = "아문-레:카드게임", Summary = "세 왕국을 오가며 그 누구보다 빠르게 피라미드를 증축하라", Price = 28500, PromotionPrice = 24200 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 60, CategoryId = 3, ProductName = "해독제", Summary = "단 하나의 해독제를 찾기 위한 치열한 추리게임", Price = 17000, PromotionPrice = 13600 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 61, CategoryId = 3, ProductName = "레지스탕스 아발론", Summary = "마피아 게임의 대명사", Price = 22000, PromotionPrice = 15400 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 62, CategoryId = 3, ProductName = "펭귄파티", Summary = "카드를 최대한 많이 내려놓고 펭귄 파티를 여세요", Price = 15000, PromotionPrice = 10000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 63, CategoryId = 3, ProductName = "셀레스티아", Summary = "하늘 위의 섬에서 펼쳐지는 대모험. 가즈아!", Price = 35000, PromotionPrice = 28000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 64, CategoryId = 3, ProductName = "우노", Summary = "전 세계적으로 큰 붐을 불러일으킨 베스트셀러 카드게임", Price = 14000, PromotionPrice = 9800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 65, CategoryId = 4, ProductName = "반지 전쟁", Summary = "반지의 제왕 3부작에 기반을 둔 보드게임", Price = 109000, PromotionPrice = 85000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 66, CategoryId = 4, ProductName = "온마스", Summary = "화성을 테라포밍하라", Price = 127000, PromotionPrice = 101600 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 67, CategoryId = 4, ProductName = "좀비사이드", Summary = "좀비보다 더 무서운 영웅들이 온다", Price = 104000, PromotionPrice = 83200 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 68, CategoryId = 4, ProductName = "하트 오브 크라운", Summary = "왕위에 오르는 공주는 누구인가", Price = 59000, PromotionPrice = 47200 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 69, CategoryId = 4, ProductName = "데드 오브 윈터", Summary = "좀비 서바이벌 게임의 최고봉", Price = 69000, PromotionPrice = 45500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 70, CategoryId = 4, ProductName = "아카디아 퀘스트", Summary = "화제의 게임 아카디아 퀘스트", Price = 11600, PromotionPrice = 92800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 71, CategoryId = 4, ProductName = "디스 워 오브 마인:보드게임", Summary = "25만 단어의 스토리기반 전쟁생존 보드게임", Price = 99000, PromotionPrice = 89000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 72, CategoryId = 4, ProductName = "클루", Summary = "단서에서 사건의 진상을 파헤쳐라", Price = 41000, PromotionPrice = 30750 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 73, CategoryId = 4, ProductName = "사이언시아", Summary = "2019년 보드엠 오리지널 게임", Price = 39000, PromotionPrice = 29250 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 74, CategoryId = 4, ProductName = "아임 더 보스", Summary = "시드 잭슨의 명작 협상 게임", Price = 49000, PromotionPrice = 35000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 75, CategoryId = 4, ProductName = "화이트채플에서 온 편지", Summary = "최고의 추리게임", Price = 62000, PromotionPrice = 43500 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 76, CategoryId = 4, ProductName = "하다라", Summary = "다른 제국들이 넘볼 수 없는 강대한 제국을 만드세요", Price = 62000, PromotionPrice = 46000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 77, CategoryId = 4, ProductName = "시에라 웨스트", Summary = "서부 개척 시대를 달린다", Price = 59000, PromotionPrice = 45000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 78, CategoryId = 4, ProductName = "아컴 호러", Summary = "최고의 호러 협력 게임", Price = 86000, PromotionPrice = 60200 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 79, CategoryId = 4, ProductName = "클랭크", Summary = "잠자는 드래곤의 둥지에서 보물을 훔쳐라", Price = 60000, PromotionPrice = 42000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 80, CategoryId = 4, ProductName = "화이트홀 미스터리", Summary = "화이트채플에서 온 편지와 동시대에 일어난 또 하나의 미스터리", Price = 40000, PromotionPrice = 28000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 81, CategoryId = 4, ProductName = "사건의 재구성", Summary = "살인 사전의 진실을 밝혀라", Price = 40000, PromotionPrice = 28000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 82, CategoryId = 4, ProductName = "기도하고 일하라", Summary = "기도하고 일하는 수도원의 경제 활동", Price = 77000, PromotionPrice = 54000 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 83, CategoryId = 4, ProductName = "크툴루: 죽음마저 죽으리니", Summary = "이계의 전투 크툴루", Price = 116000, PromotionPrice = 92800 });
            context.Products.AddOrUpdate(new Models.Product { ProductId = 84, CategoryId = 4, ProductName = "갓 오브 워:카드게임", Summary = "플레이 스테이션 화제의 게임. 카드 게임으로 컴백", Price = 52000, PromotionPrice = 41600 });

            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 1, ProductId = 1, ImageUseTypeId = 1, ImageName = "1000006166_detail_045.png", ImageType = "png", ImageSize = 233956, ImagePath = "~/Images/Products/1_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 2, ProductId = 1, ImageUseTypeId = 3, ImageName = "dal1.png", ImageType = "png", ImageSize = 2540910, ImagePath = "~/Images/Products/1_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 3, ProductId = 2, ImageUseTypeId = 1, ImageName = "1564473871341m0.png", ImageType = "png", ImageSize = 492310, ImagePath = "~/Images/Products/2_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 4, ProductId = 2, ImageUseTypeId = 3, ImageName = "17b62cdc18135d5c.jpg", ImageType = "jpg", ImageSize = 2863197, ImagePath = "~/Images/Products/2_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 5, ProductId = 3, ImageUseTypeId = 1, ImageName = "1582087214578m0.jpg", ImageType = "jpg", ImageSize = 281496, ImagePath = "~/Images/Products/3_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 6, ProductId = 3, ImageUseTypeId = 3, ImageName = "59751442b99f54a0.jpg", ImageType = "jpg", ImageSize = 1612652, ImagePath = "~/Images/Products/3_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 7, ProductId = 4, ImageUseTypeId = 1, ImageName = "1582087973933m0.jpg", ImageType = "jpg", ImageSize = 296091, ImagePath = "~/Images/Products/4_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 8, ProductId = 4, ImageUseTypeId = 3, ImageName = "02bfa132b4f81963.jpg", ImageType = "jpg", ImageSize = 1391441, ImagePath = "~/Images/Products/4_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 9, ProductId = 5, ImageUseTypeId = 1, ImageName = "1583912876358m0.jpg", ImageType = "jpg", ImageSize = 162816, ImagePath = "~/Images/Products/5_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 10, ProductId = 5, ImageUseTypeId = 3, ImageName = "10d93d0dca0e5ec0.png", ImageType = "png", ImageSize = 5126010, ImagePath = "~/Images/Products/5_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 11, ProductId = 5, ImageUseTypeId = 3, ImageName = "4bcbd5f69045d311.png", ImageType = "png", ImageSize = 3860543, ImagePath = "~/Images/Products/5_T3_2.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 12, ProductId = 6, ImageUseTypeId = 1, ImageName = "스플렌더 메인.png", ImageType = "png", ImageSize = 277106, ImagePath = "~/Images/Products/6_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 13, ProductId = 6, ImageUseTypeId = 3, ImageName = "스플레더 상세.jpg", ImageType = "jpg", ImageSize = 1667722, ImagePath = "~/Images/Products/6_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 14, ProductId = 7, ImageUseTypeId = 1, ImageName = "찬란한도시 메인.png", ImageType = "png", ImageSize = 276444, ImagePath = "~/Images/Products/7_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 15, ProductId = 7, ImageUseTypeId = 3, ImageName = "찬란한도시 상세.png", ImageType = "png", ImageSize = 3829764, ImagePath = "~/Images/Products/7_T3_1.png", InsertDt = DateTime.Now });

            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 16, ProductId = 8, ImageUseTypeId = 1, ImageName = "아그리콜라 메인.jpg", ImageType = "jpg", ImageSize = 128882, ImagePath = "~/Images/Products/8_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 17, ProductId = 8, ImageUseTypeId = 3, ImageName = "아그리콜라 상세.jpg", ImageType = "jpg", ImageSize = 3270578, ImagePath = "~/Images/Products/8_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 18, ProductId = 9, ImageUseTypeId = 1, ImageName = "아그리콜라 패밀리 메인.png", ImageType = "png", ImageSize = 359930, ImagePath = "~/Images/Products/9_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 19, ProductId = 9, ImageUseTypeId = 3, ImageName = "아그리콜라 패밀리 상세.png", ImageType = "png", ImageSize = 2513092, ImagePath = "~/Images/Products/9_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 20, ProductId = 10, ImageUseTypeId = 1, ImageName = "도미니언 메인.png", ImageType = "png", ImageSize = 96367, ImagePath = "~/Images/Products/10_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 21, ProductId = 10, ImageUseTypeId = 3, ImageName = "도미니언 상세.jpg", ImageType = "jpg", ImageSize = 6642606, ImagePath = "~/Images/Products/10_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 22, ProductId = 11, ImageUseTypeId = 1, ImageName = "도미니언 장막 메인.jpg", ImageType = "jpg", ImageSize = 75956, ImagePath = "~/Images/Products/11_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 23, ProductId = 11, ImageUseTypeId = 3, ImageName = "도미니언 장막 상세.jpg", ImageType = "jpg", ImageSize = 2986768, ImagePath = "~/Images/Products/11_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 24, ProductId = 12, ImageUseTypeId = 1, ImageName = "테라 미스티카 메인.png", ImageType = "png", ImageSize = 374099, ImagePath = "~/Images/Products/12_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 25, ProductId = 12, ImageUseTypeId = 3, ImageName = "테라 미스티카 상세1.png", ImageType = "png", ImageSize = 2666602, ImagePath = "~/Images/Products/12_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 26, ProductId = 12, ImageUseTypeId = 3, ImageName = "테라 미스티카 상세2.png", ImageType = "png", ImageSize = 3297147, ImagePath = "~/Images/Products/12_T3_2.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 27, ProductId = 12, ImageUseTypeId = 3, ImageName = "테라 미스티카 상세3.png", ImageType = "png", ImageSize = 3006224, ImagePath = "~/Images/Products/12_T3_3.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 28, ProductId = 13, ImageUseTypeId = 1, ImageName = "티츄 레드 메인.jpg", ImageType = "jpg", ImageSize = 246793, ImagePath = "~/Images/Products/13_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 29, ProductId = 13, ImageUseTypeId = 3, ImageName = "티츄 레드 상세.jpg", ImageType = "jpg", ImageSize = 2573095, ImagePath = "~/Images/Products/13_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 30, ProductId = 14, ImageUseTypeId = 1, ImageName = "티츄 블루 메인.jpg", ImageType = "jpg", ImageSize = 255948, ImagePath = "~/Images/Products/14_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 31, ProductId = 14, ImageUseTypeId = 3, ImageName = "티츄 블루 상세.jpg", ImageType = "jpg", ImageSize = 2573095, ImagePath = "~/Images/Products/14_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 32, ProductId = 15, ImageUseTypeId = 1, ImageName = "7원더스 메인.jpg", ImageType = "jpg", ImageSize = 101516, ImagePath = "~/Images/Products/15_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 33, ProductId = 15, ImageUseTypeId = 3, ImageName = "7원더스 상세.jpg", ImageType = "jpg", ImageSize = 2276801, ImagePath = "~/Images/Products/15_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 34, ProductId = 16, ImageUseTypeId = 1, ImageName = "파워그리드 메인.png", ImageType = "png", ImageSize = 343293, ImagePath = "~/Images/Products/16_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 35, ProductId = 16, ImageUseTypeId = 3, ImageName = "파워그리드 상세.png", ImageType = "png", ImageSize = 2967446, ImagePath = "~/Images/Products/16_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 36, ProductId = 17, ImageUseTypeId = 1, ImageName = "시즌스 메인.jpg", ImageType = "jpg", ImageSize = 277993, ImagePath = "~/Images/Products/17_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 37, ProductId = 17, ImageUseTypeId = 3, ImageName = "시즌스 상세.jpg", ImageType = "jpg", ImageSize = 5093421, ImagePath = "~/Images/Products/17_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 38, ProductId = 18, ImageUseTypeId = 1, ImageName = "에이언즈 엔드 메인.jpg", ImageType = "jpg", ImageSize = 350758, ImagePath = "~/Images/Products/18_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 39, ProductId = 18, ImageUseTypeId = 3, ImageName = "에이언즈 엔드 상세.png", ImageType = "png", ImageSize = 6762621, ImagePath = "~/Images/Products/18_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 40, ProductId = 19, ImageUseTypeId = 1, ImageName = "팬데믹 메인.jpg", ImageType = "jpg", ImageSize = 64883, ImagePath = "~/Images/Products/19_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 41, ProductId = 19, ImageUseTypeId = 3, ImageName = "팬데믹 상세.jpg", ImageType = "jpg", ImageSize = 3433549, ImagePath = "~/Images/Products/19_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 42, ProductId = 20, ImageUseTypeId = 1, ImageName = "팬데믹 레거시 시즌1 메인.jpg", ImageType = "jpg", ImageSize = 225666, ImagePath = "~/Images/Products/20_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 43, ProductId = 20, ImageUseTypeId = 3, ImageName = "팬데믹 레거시 시즌1 상세jpg.jpg", ImageType = "jpg", ImageSize = 2704703, ImagePath = "~/Images/Products/20_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 44, ProductId = 21, ImageUseTypeId = 1, ImageName = "푸에르토 리코 메인.png", ImageType = "png", ImageSize = 330764, ImagePath = "~/Images/Products/21_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 45, ProductId = 21, ImageUseTypeId = 3, ImageName = "푸에르토 리코 상세1.jpg", ImageType = "jpg", ImageSize = 480754, ImagePath = "~/Images/Products/21_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 46, ProductId = 21, ImageUseTypeId = 3, ImageName = "푸에르토 리코 상세2.jpg", ImageType = "jpg", ImageSize = 10307546, ImagePath = "~/Images/Products/21_T3_2.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 47, ProductId = 22, ImageUseTypeId = 1, ImageName = "푸에르토 리코 카드게임 메인.jpg", ImageType = "jpg", ImageSize = 49982, ImagePath = "~/Images/Products/22_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 48, ProductId = 22, ImageUseTypeId = 3, ImageName = "푸에르토 리코 카드게임 상세.jpg", ImageType = "jpg", ImageSize = 2033783, ImagePath = "~/Images/Products/22_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 49, ProductId = 23, ImageUseTypeId = 1, ImageName = "안드로이드넷러너 메인.jpg", ImageType = "jpg", ImageSize = 90115, ImagePath = "~/Images/Products/23_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 50, ProductId = 23, ImageUseTypeId = 3, ImageName = "안드로이드넷러너 상세1.jpg", ImageType = "jpg", ImageSize = 1010880, ImagePath = "~/Images/Products/23_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 51, ProductId = 23, ImageUseTypeId = 3, ImageName = "안드로이드넷러너 상세2.jpg", ImageType = "jpg", ImageSize = 506444, ImagePath = "~/Images/Products/23_T3_2.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 52, ProductId = 23, ImageUseTypeId = 3, ImageName = "안드로이드넷러너 상세3.jpg", ImageType = "jpg", ImageSize = 369360, ImagePath = "~/Images/Products/23_T3_3.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 53, ProductId = 23, ImageUseTypeId = 3, ImageName = "안드로이드넷러너 상세4.jpg", ImageType = "jpg", ImageSize = 823081, ImagePath = "~/Images/Products/23_T3_4.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 54, ProductId = 24, ImageUseTypeId = 1, ImageName = "브라스 랭커셔 메인.jpg", ImageType = "jpg", ImageSize = 211283, ImagePath = "~/Images/Products/24_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 55, ProductId = 24, ImageUseTypeId = 3, ImageName = "브라스 랭커셔 상세1.png", ImageType = "png", ImageSize = 4262829, ImagePath = "~/Images/Products/24_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 56, ProductId = 24, ImageUseTypeId = 3, ImageName = "브라스 랭커셔 상세2.png", ImageType = "png", ImageSize = 3822397, ImagePath = "~/Images/Products/24_T3_2.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 57, ProductId = 25, ImageUseTypeId = 1, ImageName = "요코하마 메인.jpg", ImageType = "jpg", ImageSize = 228722, ImagePath = "~/Images/Products/25_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 58, ProductId = 25, ImageUseTypeId = 3, ImageName = "요코하마 상세1.png", ImageType = "png", ImageSize = 2688259, ImagePath = "~/Images/Products/25_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 59, ProductId = 25, ImageUseTypeId = 3, ImageName = "요코하마 상세2.png", ImageType = "png", ImageSize = 1634419, ImagePath = "~/Images/Products/25_T3_2.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 60, ProductId = 26, ImageUseTypeId = 1, ImageName = "사이쓰 메인.png", ImageType = "png", ImageSize = 317093, ImagePath = "~/Images/Products/26_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 61, ProductId = 26, ImageUseTypeId = 3, ImageName = "사이쓰 상세.jpg", ImageType = "jpg", ImageSize = 4914445, ImagePath = "~/Images/Products/26_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 62, ProductId = 27, ImageUseTypeId = 1, ImageName = "사이쓰확장 먼곳에서온침략자 메인.jpg", ImageType = "jpg", ImageSize = 100549, ImagePath = "~/Images/Products/27_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 63, ProductId = 27, ImageUseTypeId = 3, ImageName = "사이쓰확장 먼곳에서온침략자 상세.jpg", ImageType = "jpg", ImageSize = 1629628, ImagePath = "~/Images/Products/27_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 64, ProductId = 28, ImageUseTypeId = 1, ImageName = "스컬킹 메인.jpg", ImageType = "jpg", ImageSize = 86242, ImagePath = "~/Images/Products/28_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 65, ProductId = 28, ImageUseTypeId = 3, ImageName = "스컬킹 상세.jpg", ImageType = "jpg", ImageSize = 4213530, ImagePath = "~/Images/Products/28_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 66, ProductId = 29, ImageUseTypeId = 1, ImageName = "라스베가스 메인.jpg", ImageType = "jpg", ImageSize = 148744, ImagePath = "~/Images/Products/29_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 67, ProductId = 29, ImageUseTypeId = 3, ImageName = "라스베가스 상세.jpg", ImageType = "jpg", ImageSize = 2519931, ImagePath = "~/Images/Products/29_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 68, ProductId = 30, ImageUseTypeId = 1, ImageName = "잭스님트 메인.jpg", ImageType = "jpg", ImageSize = 90947, ImagePath = "~/Images/Products/30_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 69, ProductId = 30, ImageUseTypeId = 3, ImageName = "잭스님트 상세.jpg", ImageType = "jpg", ImageSize = 4068128, ImagePath = "~/Images/Products/30_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 70, ProductId = 31, ImageUseTypeId = 1, ImageName = "보난자 메인.png", ImageType = "png", ImageSize = 185407, ImagePath = "~/Images/Products/31_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 71, ProductId = 31, ImageUseTypeId = 3, ImageName = "보난자 상세.jpg", ImageType = "jpg", ImageSize = 2077426, ImagePath = "~/Images/Products/31_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 72, ProductId = 32, ImageUseTypeId = 1, ImageName = "티켓 투 라이드 - 유럽 메인.png", ImageType = "png", ImageSize = 430925, ImagePath = "~/Images/Products/32_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 73, ProductId = 32, ImageUseTypeId = 3, ImageName = "티켓 투 라이드 - 유럽 상세.jpg", ImageType = "jpg", ImageSize = 2918678, ImagePath = "~/Images/Products/32_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 74, ProductId = 33, ImageUseTypeId = 1, ImageName = "카탄의 개척자 메인.jpg", ImageType = "jpg", ImageSize = 68674, ImagePath = "~/Images/Products/33_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 75, ProductId = 33, ImageUseTypeId = 3, ImageName = "카탄의 개척자 상세.jpg", ImageType = "jpg", ImageSize = 4923019, ImagePath = "~/Images/Products/33_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 76, ProductId = 34, ImageUseTypeId = 1, ImageName = "카르카손 메인.jpg", ImageType = "jpg", ImageSize = 71991, ImagePath = "~/Images/Products/34_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 77, ProductId = 34, ImageUseTypeId = 3, ImageName = "카르카손 상세.jpg", ImageType = "jpg", ImageSize = 2468743, ImagePath = "~/Images/Products/34_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 78, ProductId = 35, ImageUseTypeId = 1, ImageName = "차이나타운 메인.jpg", ImageType = "jpg", ImageSize = 70989, ImagePath = "~/Images/Products/35_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 79, ProductId = 35, ImageUseTypeId = 3, ImageName = "차이나타운 상세.jpg", ImageType = "jpg", ImageSize = 3727559, ImagePath = "~/Images/Products/35_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 80, ProductId = 36, ImageUseTypeId = 1, ImageName = "딕싯 메인.jpg", ImageType = "jpg", ImageSize = 93734, ImagePath = "~/Images/Products/36_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 81, ProductId = 36, ImageUseTypeId = 3, ImageName = "딕싯 상세.jpg", ImageType = "jpg", ImageSize = 978963, ImagePath = "~/Images/Products/36_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 82, ProductId = 37, ImageUseTypeId = 1, ImageName = "브룸 서비스 메인.jpg", ImageType = "jpg", ImageSize = 82620, ImagePath = "~/Images/Products/37_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 83, ProductId = 37, ImageUseTypeId = 3, ImageName = "브룸 서비스 상세1.jpg", ImageType = "jpg", ImageSize = 1173460, ImagePath = "~/Images/Products/37_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 84, ProductId = 37, ImageUseTypeId = 3, ImageName = "브룸 서비스 상세2.jpg", ImageType = "jpg", ImageSize = 3816995, ImagePath = "~/Images/Products/37_T3_2.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 85, ProductId = 37, ImageUseTypeId = 3, ImageName = "브룸 서비스 상세3.jpg", ImageType = "jpg", ImageSize = 3788908, ImagePath = "~/Images/Products/37_T3_3.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 86, ProductId = 38, ImageUseTypeId = 1, ImageName = "러브 레터 메인.jpg", ImageType = "jpg", ImageSize = 54067, ImagePath = "~/Images/Products/38_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 87, ProductId = 38, ImageUseTypeId = 3, ImageName = "러브 레터 상세.png", ImageType = "png", ImageSize = 1436797, ImagePath = "~/Images/Products/38_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 88, ProductId = 39, ImageUseTypeId = 1, ImageName = "마닐라 메인.png", ImageType = "png", ImageSize = 429320, ImagePath = "~/Images/Products/39_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 89, ProductId = 39, ImageUseTypeId = 3, ImageName = "마닐라 상세.png", ImageType = "png", ImageSize = 5285195, ImagePath = "~/Images/Products/39_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 90, ProductId = 40, ImageUseTypeId = 1, ImageName = "패치워크 메인.png", ImageType = "png", ImageSize = 379880, ImagePath = "~/Images/Products/40_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 91, ProductId = 40, ImageUseTypeId = 3, ImageName = "패치워크 상세.png", ImageType = "png", ImageSize = 3443285, ImagePath = "~/Images/Products/40_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 92, ProductId = 41, ImageUseTypeId = 1, ImageName = "루미큐브 메인.jpg", ImageType = "jpg", ImageSize = 71384, ImagePath = "~/Images/Products/41_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 93, ProductId = 41, ImageUseTypeId = 3, ImageName = "루미큐브 상세.jpg", ImageType = "jpg", ImageSize = 1770721, ImagePath = "~/Images/Products/41_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 94, ProductId = 42, ImageUseTypeId = 1, ImageName = "우봉고 메인.png", ImageType = "png", ImageSize = 375053, ImagePath = "~/Images/Products/42_T1_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 95, ProductId = 42, ImageUseTypeId = 3, ImageName = "우봉고 상세.png", ImageType = "png", ImageSize = 1691739, ImagePath = "~/Images/Products/42_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 96, ProductId = 43, ImageUseTypeId = 1, ImageName = "우봉고 3D 메인.jpg", ImageType = "jpg", ImageSize = 136959, ImagePath = "~/Images/Products/43_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 97, ProductId = 43, ImageUseTypeId = 3, ImageName = "우봉고 3D 상세.jpg", ImageType = "jpg", ImageSize = 741118, ImagePath = "~/Images/Products/43_T3_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 98, ProductId = 44, ImageUseTypeId = 1, ImageName = "카멜업 메인.jpg", ImageType = "jpg", ImageSize = 94187, ImagePath = "~/Images/Products/44_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 99, ProductId = 44, ImageUseTypeId = 3, ImageName = "카멜업 상세1.png", ImageType = "png", ImageSize = 2156628, ImagePath = "~/Images/Products/44_T3_1.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 100, ProductId =44, ImageUseTypeId = 3, ImageName = "카멜업 상세2.png", ImageType = "png", ImageSize = 2192686, ImagePath = "~/Images/Products/44_T3_2.png", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 101, ProductId =45, ImageUseTypeId = 1, ImageName = "더 마인드 메인.jpg", ImageType = "jpg", ImageSize = 107755, ImagePath = "~/Images/Products/45_T1_1.jpg", InsertDt = DateTime.Now });
            context.ProductImages.AddOrUpdate(new Models.ProductImage { ProductImageId = 102, ProductId =45, ImageUseTypeId = 3, ImageName = "더 마인드 상세.jpg", ImageType = "jpg", ImageSize = 1154725, ImagePath = "~/Images/Products/45_T3_1.jpg", InsertDt = DateTime.Now });

            context.OrderTypes.AddOrUpdate(new Models.OrderType { OrderTypeId = 1, Type = "주문완료" });
            context.OrderTypes.AddOrUpdate(new Models.OrderType { OrderTypeId = 2, Type = "결제대기" });
            context.OrderTypes.AddOrUpdate(new Models.OrderType { OrderTypeId = 3, Type = "결제완료" });
            context.OrderTypes.AddOrUpdate(new Models.OrderType { OrderTypeId = 4, Type = "배송중" });
            context.OrderTypes.AddOrUpdate(new Models.OrderType { OrderTypeId = 5, Type = "배송완료" });


        }
    }
}
