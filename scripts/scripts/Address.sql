CREATE PROCEDURE [dbo].[AddAddress]
    @Street VARCHAR(255),
    @City VARCHAR(100)
AS
BEGIN
    -- Insert statement to add a new address
    INSERT INTO Addresses (Street, City)
    VALUES (@Street, @City);

    -- Return the newly generated AddressID
    SELECT SCOPE_IDENTITY() AS AddressID;
END;



CREATE PROCEDURE [dbo].[UpdateAddress]
    @AddressID INT,
    @Street VARCHAR(255),
    @City VARCHAR(100)
AS
BEGIN
    UPDATE Addresses
    SET Street = @Street,
        City = @City
    WHERE AddressID = @AddressID;
END;


CREATE PROCEDURE [dbo].[ReadAddress]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT a.AddressID, a.Street, a.City
    FROM  Addresses a
END


CREATE PROCEDURE [dbo].[DeleteAddress]
    @AddressID INT
AS
BEGIN
    -- Check if the address is referenced by other tables
    IF NOT EXISTS (SELECT * FROM Companies WHERE AddressID = @AddressID)
        AND NOT EXISTS (SELECT * FROM Contacts WHERE AddressID = @AddressID)
    BEGIN
        DELETE FROM Addresses WHERE AddressID = @AddressID;
    END
END;