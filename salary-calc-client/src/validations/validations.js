export function birthDateMasking(val, prevVal) {
  // Prevent non-digit characters being entered
  if (isNaN(parseInt(val[val.length - 1], 10))) {
    return val.slice(0, -1);
  }

  // When user is deleting, this prevents immediate re-addition of '/' when it's deleted
  if (prevVal && prevVal.length >= val.length) {
    return val;
  }

  // Add / at appropriate sections of the input
  if (val.length === 2 || val.length === 5) {
    val += "/";
  }

  // Prevent characters being entered after Dob is full
  if (val.length >= 10) {
    return val.slice(0, 10);
  }

  return val;
}

export function formatAmountPHP(value) {
  const convertedInput = "PHP " + new Intl.NumberFormat().format(value);

  return convertedInput;
}

export function formatAmountRF(value) {
  if (!value) return "";

  const cleanedInput = value.replace(/,/g, "").replace(/PHP /g, "");

  if (cleanedInput.length === 0) return "";

  if (isNaN(parseInt(value[value.length - 1], 10))) {
    return this.formatAmountRF(value.slice(0, -1));
  }

  const convertedInput = "PHP " + new Intl.NumberFormat().format(cleanedInput);

  return convertedInput;
}

export function normalizeAmountRF(value) {
  return value.replace(/,/g, "").replace(/PHP /g, "");
}

export function validateDecimal(value) {
  if (!isNaN(parseFloat(value)) && isFinite(value)) {
    return true;
  }
}

export function countDecimals(value) {
  if (value % 1 === 0) return 0;
  if (Math.floor(value) !== value) {
    return value.toString().split(".")[1].length || 0;
  }
  return 0;
}
