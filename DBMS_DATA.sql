-- Query to create the User_T table, storing user information.
CREATE TABLE USER_T (
    User_ID INT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Phone_Number VARCHAR(15),
    ID_Proof VARCHAR(255)
);

-- Query to create the Patient table, storing patient-specific information and linking to the User_T table.
CREATE TABLE Patient (
    Patient_ID INT PRIMARY KEY,
    User_ID INT UNIQUE,
    Medical_History VARCHAR2(4000),
    Insurance_Details VARCHAR2(4000),
    FOREIGN KEY (User_ID) REFERENCES USER_T(User_ID) 
);

-- Query to create the ADMIN table, storing Admin-specific information and linking to the User_T table.
CREATE TABLE Admin_T (
    Admin_ID INT PRIMARY KEY,
    User_ID INT UNIQUE,
    FOREIGN KEY (User_ID) REFERENCES USER_T(User_ID) 
);



-- Query to create the Staff table, storing staff-specific information and linking to the User_T and Admin_T tables.
CREATE TABLE Staff (
    Staff_ID INT PRIMARY KEY,
    User_ID INT UNIQUE,
    Role VARCHAR(50),
    Admin_ID INT, 
    SALARY NUMBER(10,2),
    FOREIGN KEY (User_ID) REFERENCES USER_T(User_ID),
    FOREIGN KEY (Admin_ID) REFERENCES Admin_T(Admin_ID)
);
ALTER TABLE CUSTOMER_SUPPORT ADD STAFF_ID INT;

ALTER TABLE "CUSTOMER_SUPPORT"
ADD CONSTRAINT fk_staff_id
FOREIGN KEY ("STAFF_ID")
REFERENCES "STAFF" ("STAFF_ID")
ENABLE;


-- Query to create the Appointment_T table, storing appointment details and linking to the Patient and Staff tables.
CREATE TABLE Appointment_T (
    Appointment_ID INT PRIMARY KEY,
    "Date" DATE,
    "Time" TIMESTAMP, 
    Status VARCHAR(50),
    Patient_ID INT,
    Doctor_ID INT,
    FOREIGN KEY (Patient_ID) REFERENCES Patient (Patient_ID),
    FOREIGN KEY (Doctor_ID) REFERENCES Staff(Staff_ID)
);

-- Query to create the Revenue table, storing revenue information and linking to the Appointment_T table.
CREATE TABLE Revenue (
    Revenue_ID INT PRIMARY KEY,
    "Date" DATE,
    Amount DECIMAL(10,2),
    Description VARCHAR2(4000),
    Appointment_ID INT,
    FOREIGN KEY (Appointment_ID) REFERENCES Appointment_T(Appointment_ID)
);

-- Query to create the Customer_Support table, storing customer support issues and linking to the Patient table.
CREATE TABLE Customer_Support (
    Issue_ID INT PRIMARY KEY,
    Description VARCHAR2(4000), 
    Resolution VARCHAR2(4000), 
    Timestamp TIMESTAMP,
    Patient_ID INT,
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID)
);

-- Query to create the Patient_Record table, storing patient medical records and linking to the Patient table.
CREATE TABLE Patient_Record (
    Record_ID INT PRIMARY KEY,
    Patient_ID INT UNIQUE,
    Medical_History VARCHAR2(1000), 
    Insurance_Details VARCHAR2(500), 
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID)
);

-- Query to create the Profile table, storing staff profiles and linking to the Staff table.
CREATE TABLE Profile (
    Profile_ID INT PRIMARY KEY,
    Staff_ID INT UNIQUE,
    Image BLOB,
    FOREIGN KEY (Staff_ID) REFERENCES Staff(Staff_ID)
);

-- Query to create the Patient_Seat table, storing patient seat information and linking to the Appointment_T table.
CREATE TABLE Patient_Seat (
    Seat_ID INT PRIMARY KEY,
    Appointment_ID INT UNIQUE,
    Status VARCHAR(50),
    FOREIGN KEY (Appointment_ID) REFERENCES Appointment_T(Appointment_ID)
);

-- Query to create the Medical_Record table, storing medical records and linking to the Patient and Staff tables.
CREATE TABLE Medical_Record (
    Record_ID INT PRIMARY KEY,
    Patient_ID INT,
    Doctor_ID INT,
    Diagnosis VARCHAR2(1000),
    Medication VARCHAR2(1000),
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID),
    FOREIGN KEY (Doctor_ID) REFERENCES Staff(Staff_ID)
);

-- Query to create the Prescription table, storing prescription information and linking to the Patient and Staff tables.
CREATE TABLE Prescription (
    Prescription_ID INT PRIMARY KEY,
    Patient_ID INT,
    Doctor_ID INT,
    Medication VARCHAR2(1000),
    Dosage VARCHAR(50),
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID),
    FOREIGN KEY (Doctor_ID) REFERENCES Staff(Staff_ID)
);






-- Inserting user records into the USER_T table.
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(1, 'John Doe', 'john.doe@example.com', 'johndoe123', '123-456-7890', 'Driver''s License');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(2, 'Jane Smith', 'jane.smith@example.com', 'janesmith456', '987-654-3210', 'Passport');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(3, 'Michael Johnson', 'michael.johnson@example.com', 'michael123', '555-555-5555', 'National ID');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(4, 'Emily Brown', 'emily.brown@example.com', 'emilyb456', NULL, NULL);
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(5, 'Sarah Wilson', 'sarah.wilson@example.com', 'sarahw123', '111-222-3333', NULL);
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(6, 'Mark Johnson', 'mark.johnson@example.com', 'markj123', '333-444-5555', 'Driver''s License');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(7, 'Amanda Lee', 'amanda.lee@example.com', 'amandalee456', '666-777-8888', 'Passport');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(8, 'Chris Evans', 'chris.evans@example.com', 'chrise123', '999-000-1111', 'National ID');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(9, 'Olivia Martinez', 'olivia.martinez@example.com', 'oliviam456', NULL, NULL);
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(10, 'Daniel Clark', 'daniel.clark@example.com', 'danielc123', '222-333-4444', NULL);
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(11, 'Sophia Rodriguez', 'sophia.rodriguez@example.com', 'sophiar456', '777-888-9999', 'Driver''s License');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(12, 'Matthew Taylor', 'matthew.taylor@example.com', 'matthewt123', '444-555-6666', 'Passport');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(13, 'Isabella White', 'isabella.white@example.com', 'isabellaw456', '888-999-0000', 'National ID');
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(14, 'Andrew Hall', 'andrew.hall@example.com', 'andrewh123', NULL, NULL);
INSERT INTO USER_T (User_ID, Name, Email, Password, Phone_Number, ID_Proof) VALUES
(15, 'Mia Lopez', 'mia.lopez@example.com', 'mial456', '555-666-7777', NULL);


-- Inserting patient records into the Patient table.
INSERT INTO Patient (Patient_ID, User_ID, Medical_History, Insurance_Details) VALUES
(1, 6, 'Allergic to penicillin', 'XYZ Health Insurance');
INSERT INTO Patient (Patient_ID, User_ID, Medical_History, Insurance_Details) VALUES
(2, 7, 'Hypertension', 'ABC Health Insurance');
INSERT INTO Patient (Patient_ID, User_ID, Medical_History, Insurance_Details) VALUES
(3, 8, 'None', NULL);
INSERT INTO Patient (Patient_ID, User_ID, Medical_History, Insurance_Details) VALUES
(4, 9, 'Asthma', NULL);
INSERT INTO Patient (Patient_ID, User_ID, Medical_History, Insurance_Details) VALUES
(5, 10, 'Diabetes', NULL);



-- Inserting admin records into the Admin_T table.
INSERT INTO Admin_T (Admin_ID, User_ID) VALUES
(1, 1);
INSERT INTO Admin_T (Admin_ID, User_ID) VALUES
(2, 2);
INSERT INTO Admin_T (Admin_ID, User_ID) VALUES
(3, 3);
INSERT INTO Admin_T (Admin_ID, User_ID) VALUES
(4, 4);
INSERT INTO Admin_T (Admin_ID, User_ID) VALUES
(5, 5);

-- Inserting staff records into the Staff table.
INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, ADMIN_ID, SALARY)
VALUES (1, 11, 'Doctor', 1, 50000);

INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, ADMIN_ID, SALARY)
VALUES (2, 12, 'Nurse', 2, 60000);

INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, ADMIN_ID, SALARY)
VALUES (3, 13, 'Receptionist', 3, 20000);

INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, ADMIN_ID, SALARY)
VALUES (4, 14, 'Doctor', 4, 70000);

INSERT INTO HOSPITAL.STAFF (STAFF_ID, USER_ID, ROLE, ADMIN_ID, SALARY)
VALUES (5, 15, 'Nurse', 5, 50000);



-- Inserting appointment records into the Appointment_T table.
INSERT INTO Appointment_T (Appointment_ID, "Date", "Time", Status, Patient_ID, Doctor_ID) VALUES
(1, TO_DATE('2018-05-20', 'YYYY-MM-DD'), TIMESTAMP '2024-04-26 09:00:00', 'Scheduled', 1, 1);

INSERT INTO Appointment_T (Appointment_ID, "Date", "Time", Status, Patient_ID, Doctor_ID) VALUES
(2, TO_DATE('2024-11-11', 'YYYY-MM-DD'), TIMESTAMP '2024-11-11 10:00:00', 'Scheduled', 2, 4);

INSERT INTO Appointment_T (Appointment_ID, "Date", "Time", Status, Patient_ID, Doctor_ID) VALUES
(3, TO_DATE('2024-12-10', 'YYYY-MM-DD'), TIMESTAMP '2024-12-10 11:00:00', 'Scheduled', 3, 1);

INSERT INTO Appointment_T (Appointment_ID, "Date", "Time", Status, Patient_ID, Doctor_ID) VALUES
(4, TO_DATE('2024-09-15', 'YYYY-MM-DD'), TIMESTAMP '2024-09-15 13:00:00', 'Scheduled', 4, 4);

INSERT INTO Appointment_T (Appointment_ID, "Date", "Time", Status, Patient_ID, Doctor_ID) VALUES
(5, TO_DATE('2024-08-19', 'YYYY-MM-DD'), TIMESTAMP '2024-08-19 14:00:00', 'Scheduled', 5, 1);


-- Inserting customer support records into the Customer_Support table.
-- Insert record 1
INSERT INTO "CUSTOMER_SUPPORT" ("ISSUE_ID", "DESCRIPTION", "RESOLUTION", "TIMESTAMP", "PATIENT_ID", "STAFF_ID")
VALUES (1, 'Unable to login', 'Reset password', TO_TIMESTAMP('26-APR-24 10.00.00.000000 AM', 'DD-MON-YY HH:MI:SS.FF AM'), 1, 6);

-- Insert record 2
INSERT INTO "CUSTOMER_SUPPORT" ("ISSUE_ID", "DESCRIPTION", "RESOLUTION", "TIMESTAMP", "PATIENT_ID", "STAFF_ID")
VALUES (2, 'Billing discrepancy', 'Refund processed', TO_TIMESTAMP('26-APR-24 11.00.00.000000 AM', 'DD-MON-YY HH:MI:SS.FF AM'), 2, 6);

-- Insert record 3
INSERT INTO "CUSTOMER_SUPPORT" ("ISSUE_ID", "DESCRIPTION", "RESOLUTION", "TIMESTAMP", "PATIENT_ID", "STAFF_ID")
VALUES (3, 'Appointment rescheduling request', 'Rescheduled for next week', TO_TIMESTAMP('26-APR-24 12.00.00.000000 PM', 'DD-MON-YY HH:MI:SS.FF AM'), 3, 6);

-- Insert record 4
INSERT INTO "CUSTOMER_SUPPORT" ("ISSUE_ID", "DESCRIPTION", "RESOLUTION", "TIMESTAMP", "PATIENT_ID", "STAFF_ID")
VALUES (4, 'Prescription refill request', 'Prescription sent to pharmacy', TO_TIMESTAMP('26-APR-24 01.00.00.000000 PM', 'DD-MON-YY HH:MI:SS.FF AM'), 4, 6);

-- Insert record 5
INSERT INTO "CUSTOMER_SUPPORT" ("ISSUE_ID", "DESCRIPTION", "RESOLUTION", "TIMESTAMP", "PATIENT_ID", "STAFF_ID")
VALUES (5, 'Technical issue with patient portal', 'Issue resolved after server restart', TO_TIMESTAMP('26-APR-24 02.00.00.000000 PM', 'DD-MON-YY HH:MI:SS.FF AM'), 5, 6);



-- Inserting revenue records into the Revenue table.
INSERT INTO Revenue (Revenue_ID, "Date", Amount, Description, Appointment_ID) VALUES
(1, DATE '2024-04-01', 5000.00, 'Monthly revenue', 1);
INSERT INTO Revenue (Revenue_ID, "Date", Amount, Description, Appointment_ID) VALUES
(2, DATE '2024-04-02', 3500.00, 'Consultation fees', 2);
INSERT INTO Revenue (Revenue_ID, "Date", Amount, Description, Appointment_ID) VALUES
(3, DATE '2024-04-03', 2000.00, 'Insurance reimbursements', 3);
INSERT INTO Revenue (Revenue_ID, "Date", Amount, Description, Appointment_ID) VALUES
(4, DATE '2024-04-04', 6000.00, 'Medication sales', 4);
INSERT INTO Revenue (Revenue_ID, "Date", Amount, Description, Appointment_ID) VALUES
(5, DATE '2024-04-05', 4500.00, 'Service charges', 5);


-- Inserting patient record details into the Patient_Record table.
INSERT INTO Patient_Record (Record_ID, Patient_ID, Medical_History, Insurance_Details) VALUES
(1, 1, 'Allergic to penicillin', 'XYZ Health Insurance');
INSERT INTO Patient_Record (Record_ID, Patient_ID, Medical_History, Insurance_Details) VALUES
(2, 2, 'Hypertension', 'ABC Health Insurance');
INSERT INTO Patient_Record (Record_ID, Patient_ID, Medical_History, Insurance_Details) VALUES
(3, 3, 'None', NULL);
INSERT INTO Patient_Record (Record_ID, Patient_ID, Medical_History, Insurance_Details) VALUES
(4, 4, 'Asthma', NULL);
INSERT INTO Patient_Record (Record_ID, Patient_ID, Medical_History, Insurance_Details) VALUES
(5, 5, 'Diabetes', NULL);


-- Inserting profile records into the Profile table.
INSERT INTO Profile (Profile_ID, Staff_ID, Image) VALUES
(1, 1, NULL);
INSERT INTO Profile (Profile_ID, Staff_ID, Image) VALUES
(2, 2, NULL);
INSERT INTO Profile (Profile_ID, Staff_ID, Image) VALUES
(3, 3, NULL);
INSERT INTO Profile (Profile_ID, Staff_ID, Image) VALUES
(4, 4, NULL);
INSERT INTO Profile (Profile_ID, Staff_ID, Image) VALUES
(5, 5, NULL);


-- Inserting patient seat records into the Patient_Seat table.
INSERT INTO Patient_Seat (Seat_ID, Appointment_ID, Status) VALUES
(1, 1, 'Booked');
INSERT INTO Patient_Seat (Seat_ID, Appointment_ID, Status) VALUES
(2, 2, 'Booked');
INSERT INTO Patient_Seat (Seat_ID, Appointment_ID, Status) VALUES
(3, 3, 'Booked');
INSERT INTO Patient_Seat (Seat_ID, Appointment_ID, Status) VALUES
(4, 4, 'Booked');
INSERT INTO Patient_Seat (Seat_ID, Appointment_ID, Status) VALUES
(5, 5, 'Booked');


-- Inserting medical records into the Medical_Record table.
INSERT INTO Medical_Record (Record_ID, Patient_ID, Doctor_ID, Diagnosis, Medication) VALUES
(1, 1, 1, 'Sinusitis', 'Antibiotics');
INSERT INTO Medical_Record (Record_ID, Patient_ID, Doctor_ID, Diagnosis, Medication) VALUES
(2, 2, 2, 'Hypertension', 'Lisinopril');
INSERT INTO Medical_Record (Record_ID, Patient_ID, Doctor_ID, Diagnosis, Medication) VALUES
(3, 3, 3, NULL, NULL);
INSERT INTO Medical_Record (Record_ID, Patient_ID, Doctor_ID, Diagnosis, Medication) VALUES
(4, 4, 4, 'Asthma exacerbation', 'Inhaler');
INSERT INTO Medical_Record (Record_ID, Patient_ID, Doctor_ID, Diagnosis, Medication) VALUES
(5, 5, 5, 'Type 2 Diabetes', 'Metformin');


-- Inserting prescription records into the Prescription table.
INSERT INTO Prescription (Prescription_ID, Patient_ID, Doctor_ID, Medication, Dosage) VALUES
(1, 1, 1, 'Antibiotics', '500mg');
INSERT INTO Prescription (Prescription_ID, Patient_ID, Doctor_ID, Medication, Dosage) VALUES
(2, 2, 2, 'Lisinopril', '10mg');
INSERT INTO Prescription (Prescription_ID, Patient_ID, Doctor_ID, Medication, Dosage) VALUES
(3, 3, 3, 'Aspirin', '100mg');
INSERT INTO Prescription (Prescription_ID, Patient_ID, Doctor_ID, Medication, Dosage) VALUES
(4, 4, 4, 'Albuterol', '2 puffs every 4 hours');
INSERT INTO Prescription (Prescription_ID, Patient_ID, Doctor_ID, Medication, Dosage) VALUES
(5, 5, 5, 'Insulin', '10 units before meals');
