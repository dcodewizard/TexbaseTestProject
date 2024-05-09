CREATE PROCEDURE [dbo].[CreateContact]
    @ContactName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Title NVARCHAR(100),
    @Street NVARCHAR(100),
    @City NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AddressID INT, @ContactID INT;

    IF NOT EXISTS (SELECT 1 FROM Addresses WHERE Street = @Street AND City = @City)
    BEGIN
        INSERT INTO Addresses (Street, City)
        VALUES (@Street, @City);
        
        SET @AddressID = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT @AddressID = AddressID FROM Addresses WHERE Street = @Street AND City = @City;
    END

    INSERT INTO Contacts (ContactName,Email,Title, AddressID)
    VALUES (@ContactName,@Email,@Title, @AddressID);

    SET @ContactID = SCOPE_IDENTITY();   
END


CREATE PROCEDURE [dbo].[ReadContacts]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.ContactID, c.ContactName,c.Email,c.Title, a.AddressID, a.Street, a.City
    FROM Contacts c
    INNER JOIN Addresses a ON c.AddressID = a.AddressID;
END




CREATE PROCEDURE [dbo].[UpdateContact]
    @ContactName NVARCHAR(100),
    @ContactID INT,
    @Email NVARCHAR(100),
    @Title NVARCHAR(100),
    @Street NVARCHAR(100),
    @City NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    DECLARE @ExistingAddressID INT;

    -- Check if the provided Street and City match an existing address
    SELECT @ExistingAddressID = AddressID
    FROM Addresses
    WHERE Street = @Street AND City = @City;

    IF @ExistingAddressID IS NOT NULL
    BEGIN
        -- If an existing address is found, update the Company table's AddressID
        UPDATE Contacts
        SET AddressID = @ExistingAddressID,
            ContactName = @ContactName,
			Email=@Email,
			Title=@Title
        WHERE ContactID = @ContactID;
    END
    ELSE
    BEGIN
        -- If no existing address is found, insert a new address
        INSERT INTO Addresses (Street, City)
        VALUES (@Street, @City);

        -- Get the newly inserted address's AddressID
        SELECT @ExistingAddressID = SCOPE_IDENTITY();

        -- Update the Company table's AddressID with the new AddressID
        UPDATE Contacts
        SET AddressID = @ExistingAddressID,
      ContactName = @ContactName,
			Email=@Email,
			Title=@Title 
			WHERE ContactID = @ContactID;
    END

    COMMIT TRANSACTION;
END



CREATE PROCEDURE [dbo].[DeleteContact]
    @ContactID INT
AS
BEGIN
    DELETE FROM Contacts
    WHERE ContactID = @ContactID;
END;
