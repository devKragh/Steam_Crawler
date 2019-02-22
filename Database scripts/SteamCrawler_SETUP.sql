USE Steam_Crawler;

IF OBJECT_ID('dbo.GamePlatform', 'U') IS NOT NULL 
  DROP TABLE dbo.GamePlatform;

IF OBJECT_ID('dbo.Gameoffer', 'U') IS NOT NULL 
  DROP TABLE dbo.Gameoffer;

IF OBJECT_ID('dbo.Platform', 'U') IS NOT NULL 
  DROP TABLE dbo.[Platform];

CREATE TABLE [Platform](
	id INT PRIMARY KEY IDENTITY(1,1),
	platform_name VARCHAR(15) NOT NULL
)

CREATE TABLE Gameoffer(
	id INT PRIMARY KEY,
	title VARCHAR(100) NOT NULL,
	current_price DECIMAL(5, 2),
	current_discount_price DECIMAL(5, 2),
	current_discount_percentage TINYINT,
	userreviewscore_percentage TINYINT,
	steam_store_link VARCHAR(150) NOT NULL
)

CREATE TABLE GamePlatform(
	gameoffer_id INT NOT NULL,
	platform_id INT NOT NULL,
	PRIMARY KEY (gameoffer_id, platform_id),
	FOREIGN KEY (gameoffer_id) REFERENCES Gameoffer(id),
	FOREIGN KEY (platform_id) REFERENCES [Platform](id)
)

INSERT INTO [Platform] VALUES ('win'), ('mac'), ('linux')

use master;