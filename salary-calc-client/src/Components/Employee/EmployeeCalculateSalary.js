import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { calculateSalary } from "../../actions";
import {
  validateDecimal,
  countDecimals,
  formatAmountPHP,
} from "../../validations/validations";
import { EMPLOYEE_TYPE } from "../../Constants/constants";
import moment from "moment";

const EmployeeCalculateSalary = ({ currentEmployeeId }) => {
  const employee = useSelector((state) =>
    currentEmployeeId
      ? state.employees.find((emp) => emp.employeeId === currentEmployeeId)
      : null
  );
  const [effectiveDays, setEffectiveDays] = useState(employee.effectiveDays);
  const [errMsg, setErrMsg] = useState("");

  const dispatch = useDispatch();

  useEffect(() => {
    setErrMsg("");
  }, [currentEmployeeId]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (effectiveDays === "" || effectiveDays === null) {
      setErrMsg("Work Days is a required field.");
      return;
    }

    const submitValues = {
      employeeId: currentEmployeeId,
      employeeType: employee.employeeType,
      effectiveDays: parseFloat(effectiveDays).toFixed(2),
      completed: 1,
    };

    console.log(submitValues);

    dispatch(calculateSalary(currentEmployeeId, submitValues));
  };

  const renderError = () => {
    if (errMsg.length > 0) {
      return (
        <div className="item">
          <div className="content">
            <div className="ui error message">
              <div className="header">{errMsg}</div>
            </div>
          </div>
        </div>
      );
    }
    return "";
  };

  useEffect(() => {
    if (employee) setEffectiveDays(employee.effectiveDays);
  }, [employee]);

  const handleOnChange = (e) => {
    const value = e.target.value;

    if (value.length === 0) setEffectiveDays("");
    if (validateDecimal(value)) {
      if (countDecimals(parseFloat(value)) <= 2) setEffectiveDays(value);
    }
  };

  const renderComputedSalary = () => {
    if (employee.completed > 0) {
      return (
        <div className="item">
          <div className="content">
            <div className="ui large header">
              {employee.completed > 0
                ? formatAmountPHP(employee.computedSalary)
                : "---"}
            </div>
            <div className="meta">Computed Salary</div>
          </div>
        </div>
      );
    }

    return "";
  };

  return (
    <div className="ui grid">
      <div className="one wide column"></div>
      <div className="fourteen wide column">
        <form className="ui form error" onSubmit={handleSubmit}>
          <div className="ui divided items">
            <div className="item">
              <div className="content">
                <div className="ui large header">{employee.name}</div>
                <div className="meta">Name</div>
              </div>
            </div>
            <div className="item">
              <div className="content">
                <div className="ui two column grid">
                  <div className="ui column">
                    <div className="ui medium header">
                      {EMPLOYEE_TYPE[employee.employeeType]}
                    </div>
                    <div className="meta">Employee Type</div>
                  </div>
                  <div className="ui column">
                    <div className="ui medium header">
                      {formatAmountPHP(employee.baseSalary)}
                    </div>
                    <div className="meta">
                      {employee.employeeType === 1
                        ? "Rate Per Day"
                        : "Salary Per Month"}
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="item">
              <div className="content">
                <div className="ui two column grid">
                  <div className="ui column">
                    <div className="ui medium header">
                      {moment(employee.birthdate).format("MMMM DD, YYYY")}
                    </div>
                    <div className="meta">Birth Date</div>
                  </div>
                  <div className="ui column">
                    <div className="ui medium header">{employee.tin}</div>
                    <div className="meta">TIN</div>
                  </div>
                </div>
              </div>
            </div>
            <div className="item">
              <div className="content">
                <div className="ui medium header">
                  {employee.employeeType === 1 ? "Work Days" : "Days Absent"}
                </div>
                <div className="ui two column grid">
                  <div className="column">
                    <input
                      type="text"
                      placeholder="0.00"
                      value={effectiveDays}
                      onChange={handleOnChange}
                    ></input>
                  </div>

                  <div className="ui column">
                    <button className="ui violet labeled icon large button">
                      <i className="calculator icon"></i>
                      Calculate
                    </button>
                  </div>
                </div>
              </div>
            </div>
            {renderComputedSalary()}
            {renderError()}
          </div>
        </form>
      </div>
    </div>
  );
};

export default EmployeeCalculateSalary;
