/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./wwwroot/**/*.{html,cshtml,cs,js}",
    "./Views/**/*.{html,cshtml,cs,js}",
  ],
  theme: {
    extend: {},
  },
  plugins: [require("flowbite/plugin")],
};
