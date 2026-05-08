 ALTER PROCEDURE sp_GetDoctors
(
    @Search NVARCHAR(100) = NULL,
    @Status INT = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 10
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        FullName,
        Email,
        Specialization,
        LicenseNumber,
        LicenseExpiryDate,
        CASE
            WHEN LicenseExpiryDate < CAST(GETDATE() AS DATE) THEN 'Expired'
            WHEN Status = 3 THEN 'Suspended'
            ELSE 'Active'
        END AS Status,
        CreatedDate
    FROM Doctors
    WHERE IsDeleted = 0
      AND (
            @Search IS NULL
            OR @Search = ''
            OR FullName LIKE '%' + @Search + '%'
            OR LicenseNumber LIKE '%' + @Search + '%'
          )
      AND (
            @Status IS NULL
            OR @Status = 0
            OR (@Status = 2 AND LicenseExpiryDate < CAST(GETDATE() AS DATE))
            OR (@Status = 1 AND LicenseExpiryDate >= CAST(GETDATE() AS DATE) AND Status = 1)
            OR (@Status = 3 AND Status = 3)
          )
    ORDER BY CreatedDate DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    SELECT COUNT(1) AS TotalCount
    FROM Doctors
    WHERE IsDeleted = 0
      AND (
            @Search IS NULL
            OR @Search = ''
            OR FullName LIKE '%' + @Search + '%'
            OR LicenseNumber LIKE '%' + @Search + '%'
          )
      AND (
            @Status IS NULL
            OR @Status = 0
            OR (@Status = 2 AND LicenseExpiryDate < CAST(GETDATE() AS DATE))
            OR (@Status = 1 AND LicenseExpiryDate >= CAST(GETDATE() AS DATE) AND Status = 1)
            OR (@Status = 3 AND Status = 3)
          );
END