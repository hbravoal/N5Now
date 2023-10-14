const withMT = require("@material-tailwind/react/utils/withMT");

module.exports = withMT({
  content: ["./src/**/*.{js,jsx,ts,tsx}",
  "./node_modules/tailwind-datepicker-react/dist/**/*.js", // <--- Add this line
],
  theme: {
    extend: {},
  },
  plugins: [],
});