IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DevsuAppDb')
BEGIN
    CREATE DATABASE DevsuAppDb;
    PRINT 'Database DevsuAppDb created successfully.';
END
ELSE
BEGIN
    PRINT 'Database DevsuAppDb already exists.';
END
