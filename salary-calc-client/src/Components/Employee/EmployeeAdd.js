import React from "react";
import { reset, Field, reduxForm } from "redux-form";
import { connect } from "react-redux";
import { createEmployee } from "../../actions";
import moment from "moment";
import {
  birthDateMasking,
  formatAmountRF,
  normalizeAmountRF,
} from "../../validations/validations";

class EmployeeAdd extends React.Component {
  componentDidMount() {
    // Assign fetchProducts into your Component
    this.birthDateMasking = birthDateMasking.bind(this);
    this.formatAmountRF = formatAmountRF.bind(this);
    this.normalizeAmountRF = normalizeAmountRF.bind(this);
  }

  onSubmit = (formValues, dispatch) => {
    const {
      lastName,
      firstName,
      middleName,
      birthDate,
      tin,
      employeeType,
      baseSalary,
    } = formValues;

    const submitValues = {
      name: `${lastName}, ${firstName} ${middleName[0]}.`,
      birthdate: moment(birthDate, "MM/DD/YYYY", true).toISOString(),
      tin: tin,
      employeeType: parseInt(employeeType),
      baseSalary: parseFloat(baseSalary).toFixed(2),
    };

    this.props.createEmployee(submitValues);
    dispatch(reset("EmployeeAdd"));
  };

  renderError({ error, touched }) {
    if (touched && error) {
      return <div className="ui pointing red basic label">{error}</div>;
    }
  }

  renderInput = ({ input, label, meta, placeholder }) => {
    const className = `field ${meta.error && meta.touched ? "error" : ""}`;
    return (
      <div className={className}>
        <label>{label}</label>
        <input {...input} placeholder={placeholder} />
        {this.renderError(meta)}
      </div>
    );
  };

  render() {
    return (
      <form
        onSubmit={this.props.handleSubmit(this.onSubmit)}
        className="ui form error"
        autoComplete="off"
      >
        <Field
          name="firstName"
          component={this.renderInput}
          label="First Name"
          placeholder="First Name"
        />
        <Field
          name="middleName"
          component={this.renderInput}
          label="Middle Name"
          placeholder="Middle Name"
        />
        <Field
          name="lastName"
          component={this.renderInput}
          label="Last Name"
          placeholder="Last Name"
        />

        <div className="two fields">
          <div className="six wide field">
            <Field
              name="birthDate"
              component={this.renderInput}
              label="Birth Date"
              placeholder="MM/DD/YYYY"
              normalize={this.birthDateMasking}
            />
          </div>
          <div className="five wide field">
            <Field
              name="baseSalary"
              component={this.renderInput}
              label="Base Salary"
              placeholder="Base Salary"
              format={this.formatAmountRF}
              normalize={this.normalizeAmountRF}
            />
          </div>
          <div className="five wide field">
            <Field
              name="tin"
              component={this.renderInput}
              label="TIN"
              placeholder="TIN"
            />
          </div>
        </div>
        <div className="inline fields">
          <label>Employmee Type:</label>
          <div className="field">
            <Field
              name="employeeType"
              component="input"
              type="radio"
              value="1"
            />
            <label>Contractual</label>
          </div>
          <div className="field">
            <Field
              name="employeeType"
              component="input"
              type="radio"
              value="2"
            />
            <label>Regular</label>
          </div>
        </div>
        <div className="extra content">
          <button className="ui floated button violet">Submit</button>
        </div>
      </form>
    );
  }
}

const validate = (formValues) => {
  const errors = {};

  if (!formValues.firstName) {
    errors.firstName = "You must enter a first name";
  }

  if (!formValues.lastName) {
    errors.lastName = "You must enter a last name";
  }

  if (!formValues.middleName) {
    errors.middleName = "You must enter a middle name";
  }

  const validDate = moment(formValues.birthDate, "MM/DD/YYYY", true).isValid();

  if (validDate) {
    if (new Date(formValues.birthDate) > new Date()) {
      errors.birthDate = "Cannot be future dated";
    }
  } else {
    errors.birthDate = "Invalid date";
  }

  if (!formValues.employeeType) {
    errors.employeeType = "You must enter an employee type";
  }

  if (!formValues.baseSalary) {
    errors.baseSalary = "You must enter a base salary.";
  }

  return errors;
};

const wrapComponent = reduxForm({
  form: "EmployeeAdd",
  validate,
})(EmployeeAdd);

export default connect(null, { createEmployee })(wrapComponent);
