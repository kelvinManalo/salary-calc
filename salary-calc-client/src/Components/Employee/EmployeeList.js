import React from "react";
import { useSelector } from "react-redux";
import { EMPLOYEE_TYPE } from "../../Constants/constants";
import MediumTableLoader from "../Loaders/MediumTableLoader";

const EmployeeList = ({ setCurrentEmployeeId, currentEmployeeId }) => {
  const employees = useSelector((state) => {
    return state.employees;
  });

  if (!employees) {
    return (
      <div className="nine wide column">
        <MediumTableLoader />
      </div>
    );
  }

  if (employees.length === 0) {
    return (
      <div className="nine wide column">
        <div className="ui grid middle aligned">
          <div className="row">
            <div className="column">
              <div className="ui text container segment">
                <h2 className="ui middle aligned icon header">
                  <i className="circular users icon"></i>
                  Add a new Employee To Start
                </h2>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  const renderRows = () => {
    const renderStatus = (completed) => {
      if (completed > 0)
        return (
          <>
            <i className="icon checkmark"></i> Completed
          </>
        );
      else
        return (
          <>
            <i className="icon ellipsis horizontal"></i> Pending
          </>
        );
    };

    return employees.map(({ employeeId, name, employeeType, completed }) => {
      return (
        <tr
          className={completed > 0 ? "positive" : ""}
          onClick={() => setCurrentEmployeeId(employeeId)}
          key={employeeId}
        >
          <td>{name}</td>
          <td>
            <i
              className={
                employeeType === 1 ? "icon outline user" : "icon user "
              }
            ></i>
            {EMPLOYEE_TYPE[employeeType]}
          </td>
          <td>{renderStatus(completed)}</td>
        </tr>
      );
    });
  };

  return (
    <div className="nine wide column">
      <div className="ui container">
        <table className="ui celled selectable table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Employee Type</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>{renderRows()}</tbody>
          <tfoot className="full-width">
            <tr className="right aligned">
              <th colSpan="3">
                <div className="extra content">
                  <button
                    onClick={() => setCurrentEmployeeId(null)}
                    className={`ui violet labeled icon ${
                      currentEmployeeId === null ? "disabled" : ""
                    } button`}
                  >
                    <i className="user icon"></i> Add Employee
                  </button>
                </div>
              </th>
            </tr>
          </tfoot>
        </table>
      </div>
    </div>
  );
};

export default EmployeeList;
