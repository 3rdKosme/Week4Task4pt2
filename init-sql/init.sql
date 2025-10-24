IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Authors')
BEGIN
    CREATE TABLE Authors (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        DateOfBirth DATE NOT NULL
    );
    PRINT 'Таблица Authors создана.';
END
ELSE
BEGIN   
    PRINT 'Таблица Authors уже существует.';
END
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books')
BEGIN
	CREATE TABLE Books (
		Id INT IDENTITY(1,1),
        Title NVARCHAR(200) NOT NULL,
        PublishedYear INT NOT NULL,
        AuthorId INT NOT NULL,
        
        CONSTRAINT FK_Books_Authors
			FOREIGN KEY (AuthorId) REFERENCES Authors(Id)
            ON DELETE CASCADE
    );
    PRINT 'Таблица Books создана.';
END
ELSE
BEGIN
	PRINT 'Таблица Books уже существует.';
END

IF NOT EXISTS (SELECT 1 FROM Authors)
BEGIN 
	SET IDENTITY_INSERT Authors ON;
    
    INSERT INTO Authors (Id, Name, DateOfBirth) VALUES
    (1, N'Лев Толстой', '1828-09-09'),
    (2, N'Фёдор Достоевский', '1821-11-11'),
    (3, N'Антуан де Сент-Экзюпери', '1900-06-29'),
    (4, N'Джордж Оруэлл', '1903-06-25'),
    (5, N'Харпер Ли', '1926-04-28'),
    (6, N'Габриэль Гарсиа Маркес', '1927-03-06'),
    (7, N'Джоан Роулинг', '1965-07-31'),
    (8, N'Стивен Кинг', '1947-09-21'),
    (9, N'Агата Кристи', '1890-09-15'),
    (10, N'Эрнест Хемингуэй', '1899-07-21');
    
    SET IDENTITY_INSERT Authors OFF;
END

IF NOT EXISTS (SELECT 1 FROM BOOKS)
BEGIN
	SET IDENTITY_INSERT ON;
    
	INSERT INTO Books (Id, Title, PublishedYear, AuthorId) VALUES
    (1, N'Война и мир', 1869, 1),
    (2, N'Анна Каренина', 1877, 1),
    (3, N'Преступление и наказание', 1866, 2),
    (4, N'Идиот', 1869, 2),
    (5, N'Маленький принц', 1943, 3),
    (6, N'1984', 1949, 4),
    (7, N'Скотный двор', 1945, 4),
    (8, N'Убить пересмешника', 1960, 5),
    (9, N'Сто лет одиночества', 1967, 6),
    (10, N'Гарри Поттер и философский камень', 1997, 7),
    (11, N'Оно', 1986, 8),
    (12, N'Сияние', 1977, 8),
    (13, N'Убийство в Восточном экспрессе', 1934, 9),
    (14, N'Праздник, который всегда с тобой', 1960, 10),
    (15, N'Старик и море', 1952, 10);
    
    SET IDENTITY_INSERT Books OFF;
END