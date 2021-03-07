import employees from "../apis/employees";
import {
  FETCH_EMPLOYEES,
  FETCH_EMPLOYEE,
  CREATE_EMPLOYEE,
  CALCULATE_SALARY,
} from "./types";

export const fetchEmployees = () => async (dispatch) => {
  try {
    const response = await employees.get("/employee");
    dispatch({ type: FETCH_EMPLOYEES, payload: response.data.data });
  } catch (error) {
    console.log(error);
    dispatch({ type: "LOAD_ERROR" });
  }
};

//used if we implement router
export const fetchEmployee = (id) => async (dispatch) => {
  const response = await employees.get(`/employee/${id}`);

  try {
    dispatch({ type: FETCH_EMPLOYEE, payload: response.data.data });
  } catch (error) {
    console.log(error);
    dispatch({ type: "LOAD_ERROR" });
  }
};

export const createEmployee = (formValues) => async (dispatch) => {
  const response = await employees.post("/employee", formValues);

  try {
    dispatch({ type: CREATE_EMPLOYEE, payload: response.data.data });
  } catch (error) {
    console.log(error);
    dispatch({ type: "LOAD_ERROR" });
  }
};

export const calculateSalary = (id, formValues) => async (dispatch) => {
  const response = await employees.put(`/employee/${id}`, formValues);

  console.log(response);

  try {
    dispatch({ type: CALCULATE_SALARY, payload: response.data.data });
  } catch (error) {
    console.log(error);
    dispatch({ type: "LOAD_ERROR" });
  }
};
