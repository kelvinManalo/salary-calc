import { combineReducers } from "redux";
import { reducer as formReducer } from "redux-form";

import employeeReducer from "./employeeReducer";
import errorReducer from "./errorReducer";

export default combineReducers({
  form: formReducer,
  employees: employeeReducer,
  error: errorReducer,
});
