import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { fetchEmployees } from "./actions";
import "./App.css";
import EmployeeForm from "./Components/Employee/EmployeeForm";
import Header from "./Components/Header";
import EmployeeList from "./Components/Employee/EmployeeList";
import Errorpage from "./Components/ErrorPage/ErrorPage";

const App = () => {
  const [currentEmployeeId, setCurrentEmployeeId] = useState(null);

  const error = useSelector((state) => state.error);

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchEmployees());
  }, [currentEmployeeId, dispatch]);

  const renderPage = () => {
    if (error.length === 0) {
      return (
        <>
          <EmployeeList
            setCurrentEmployeeId={setCurrentEmployeeId}
            currentEmployeeId={currentEmployeeId}
          />
          <EmployeeForm
            setCurrentEmployeeId={setCurrentEmployeeId}
            currentEmployeeId={currentEmployeeId}
          />
        </>
      );
    } else {
      return (
        <div className="ui fourteen wide column">
          <Errorpage />;
        </div>
      );
    }
  };

  return (
    <div className="ui stackable grid" style={{ height: "100vh" }}>
      <Header />
      <div className="column"></div>
      {renderPage()}
    </div>
  );
};

export default App;
