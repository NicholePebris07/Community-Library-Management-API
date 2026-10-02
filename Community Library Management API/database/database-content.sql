USE CommunityLibraryDb;
GO

INSERT INTO Books
    (Title, Author, ISBN, Category, TotalCopies, AvailableCopies)
VALUES
    ('Introduction to Programming', 'John Smith', '978-1000000001', 'Programming', 5, 5),
    ('Database Fundamentals', 'Jane Brown', '978-1000000002', 'Database', 4, 3),
    ('Web Development Basics', 'Mark Wilson', '978-1000000003', 'Web Development', 3, 2),
    ('Data Structures and Algorithms', 'Sarah Davis', '978-1000000004', 'Computer Science', 6, 6);
GO

INSERT INTO Members
    (FullName, Email, MembershipType, DateJoined, IsActive)
VALUES
    ('Alice Santos', 'alice@example.com', 'Student', '2026-09-01', 1),
    ('Brian Cruz', 'brian@example.com', 'Student', '2026-09-02', 1),
    ('Carol Reyes', 'carol@example.com', 'Faculty', '2026-09-03', 1),
    ('David Garcia', 'david@example.com', 'Student', '2026-09-04', 0);
GO

INSERT INTO Loans
    (BookId, MemberId, BorrowedDate, DueDate, ReturnedDate, Status)
VALUES
    (2, 1, '2026-09-20', '2026-09-27', NULL, 'Borrowed'),
    (3, 2, '2026-09-21', '2026-09-28', NULL, 'Borrowed'),
    (1, 3, '2026-09-10', '2026-09-17', '2026-09-16', 'Returned');
GO