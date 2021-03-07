const loadError = (state = "", action) => {
  if (action.type === "LOAD_ERROR") {
    return "ERROR";
  } else {
    return state;
  }
};

export default loadError;
