import React from "react";

const Errorpage = () => {
  return (
    <div
      className="ui grid middle aligned segment inverted"
      style={{ height: "100%", margin: "0" }}
    >
      <div className="ui column center aligned">
        <div className="ui inverted statistic">
          <div className="value">404</div>
          <div className="label">Error Encountered</div>
        </div>
      </div>
    </div>
  );
};

export default Errorpage;
