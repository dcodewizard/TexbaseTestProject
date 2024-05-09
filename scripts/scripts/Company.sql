CREATE PROCEDURE [dbo].[CreateCompany]
    @CompanyName NVARCHAR(100),
    @Street NVARCHAR(100),
    @City NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AddressID INT, @CompanyID INT;

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

    INSERT INTO Companies (CompanyName, AddressID)
    VALUES (@CompanyName, @AddressID);

    SET @CompanyID = SCOPE_IDENTITY();   
END



CREATE PROCEDURE [dbo].[ReadCompanies]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.CompanyID, c.CompanyName, a.AddressID, a.Street, a.City
    FROM Companies c
    INNER JOIN Addresses a ON c.AddressID = a.AddressID;
END


CREATE PROCEDURE [dbo].[UpdateCompany]
    @CompanyID INT,
    @CompanyName NVARCHAR(100),
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
        UPDATE Companies
        SET AddressID = @ExistingAddressID,
            CompanyName = @CompanyName
        WHERE CompanyID = @CompanyID;
    END
    ELSE
    BEGIN
        -- If no existing address is found, insert a new address
        INSERT INTO Addresses (Street, City)
        VALUES (@Street, @City);

        -- Get the newly inserted address's AddressID
        SELECT @ExistingAddressID = SCOPE_IDENTITY();

        -- Update the Company table's AddressID with the new AddressID
        UPDATE Companies
        SET AddressID = @ExistingAddressID,
            CompanyName = @CompanyName
        WHERE CompanyID = @CompanyID;
    END

    COMMIT TRANSACTION;
END




CREATE PROCEDURE [dbo].[DeleteCompany]
    @CompanyID INT
AS
BEGIN
    DELETE FROM Companies
    WHERE CompanyID = @CompanyID;
END;
