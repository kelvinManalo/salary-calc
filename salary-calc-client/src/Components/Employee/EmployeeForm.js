import React from "react";
import EmployeeAdd from "./EmployeeAdd";
import EmployeeCalculateSalary from "./EmployeeCalculateSalary";

const EmployeeForm = ({ setCurrentEmployeeId, currentEmployeeId }) => {
  return (
    <div className="five wide column">
      <div className="ui container">
        <div className="ui fluid card">
          <div className="content">
            <div className="header">
              {currentEmployeeId
                ? "Calculate Employee Salary"
                : "Add an employee"}
            </div>
          </div>
          <div className="content">
            {currentEmployeeId === null ? (
              <EmployeeAdd
                setCurrentEmployeeId={setCurrentEmployeeId}
                initialValues={{ employeeType: "1", baseSalary: "" }}
              />
            ) : (
              <EmployeeCalculateSalary currentEmployeeId={currentEmployeeId} />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default EmployeeForm;
