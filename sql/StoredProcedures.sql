-- CREATE Department ----------------------------------------------------------------
CREATE PROCEDURE usp_CreateDepartment
(
	@DName NVARCHAR(50),
	@MgrSSN INT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Department WHERE DName = @DName)
		THROW 50001, 'Department name already in use', 1
	IF EXISTS (SELECT 1 FROM Department WHERE @MgrSSN = MgrSSN)
		THROW 50002, 'This SSN is already a manager', 1
	INSERT INTO Department VALUES (@DName,@MgrSSN,GETDATE());
	SELECT SCOPE_IDENTITY();
END
-- EXECUTE 
EXEC dbo.usp_CreateDepartment @DName = DevOps, @MgrSSN = 999887777;

-- DELETE Department ----------------------------------------------------------------
CREATE PROCEDURE usp_DeleteDepartment
(
	@DNumber INT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Department WHERE DNumber = @DNumber)
		BEGIN
	
		DELETE FROM Department WHERE DNumber = @DNumber
		END
		ELSE
		THROW 50003, 'This department does not exists',1
END
-- EXECUTE 
EXEC dbo.usp_DeleteDepartment @DNumber = 1;

-- GET Department by DNumber ----------------------------------------------------
CREATE PROCEDURE usp_GetDepartment
(
	@DNumber INT
)
AS
BEGIN
	SELECT DName,DNumber,MgrSSN,MgrStartDate, COUNT(SSN) as EmpCount FROM Department
	JOIN Employee ON Dno = DNumber
	WHERE DNumber = @DNumber
	GROUP BY DName, DNumber,MgrSSN,MgrStartDate
END
-- EXECUTE 
EXEC dbo.usp_GetDepartment @DNumber = 2

-- GET All Departments ---------------------------------------------------------
CREATE PROCEDURE usp_GetAllDepartments
AS
BEGIN
	SELECT DName,DNumber,MgrSSN,MgrStartDate, COUNT(SSN) as EmpCount FROM Department
	JOIN Employee ON DNumber = Dno
	GROUP BY DName, DNumber,MgrSSN,MgrStartDate
END
-- EXECUTE
EXEC dbo.usp_GetAllDepartments 

-- UPDATE Department Name --------------------------------------------------------
CREATE PROCEDURE usp_UpdateDepartmentName
(
	@DNumber INT,
	@DName NVARCHAR(50)
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Department WHERE DName = @DName)
		THROW 50001, 'Department name already in use', 1
	UPDATE Department SET DName = @DName 
	WHERE DNumber = @DNumber;
END
-- EXECUTE 
EXEC dbo.usp_UpdateDepartmentName @DNumber = 9, @DName = 'Devops updated'

-- UPDATE Department MgrSSN -------------------------------------------------------
CREATE PROCEDURE usp_UpdateDepartmentManager
(
    @DNumber int, @MgrSSN int
) 
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Department WHERE MgrSSN = @MgrSSN)
		THROW 50001, 'MgrSSN already in use', 1
    UPDATE Department
    SET MgrSSN = @MgrSSN,
        MgrStartDate = GETDATE()
    WHERE DNumber = @DNumber

    UPDATE Employee
    SET SuperSSN = @MgrSSN
    WHERE Dno = @DNumber
    AND SSN <> @MgrSSN
END
-- EXECUTE 
EXEC dbo.usp_UpdateDepartmentManager @DNumber = 1, @MgrSSN = 453453453

SELECT * FROM Employee e WHERE e.SuperSSN = 987654321
SELECT * FROM Employee e WHERE e.SuperSSN = 999887777
SELECT * FROM Employee e2 WHERE e2.Dno = 1 --John, Ramesh, James



