import {
  FETCH_EMPLOYEES,
  FETCH_EMPLOYEE,
  CREATE_EMPLOYEE,
  CALCULATE_SALARY,
} from "../actions/types";

const employeeReducer = (state = [], action) => {
  switch (action.type) {
    case FETCH_EMPLOYEES:
    case CREATE_EMPLOYEE:
    case CALCULATE_SALARY:
      return action.payload;
    case FETCH_EMPLOYEE:
      return state;
    default:
      return state;
  }
};

export default employeeReducer;
