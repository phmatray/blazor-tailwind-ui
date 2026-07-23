/** @type {import('tailwindcss').Config} */
module.exports = {
  // Content: Paths to all files that contain Tailwind class names
  // Tailwind scans these files to generate only the CSS you actually use
  // Add any new directories here if you create them (e.g., './Pages/**/*.razor')
  content: [
    './Components/**/*.razor',   // Blazor components
    './Components/**/*.html',    // HTML files
    './Components/**/*.cshtml'   // Razor views (if using)
  ],

  // Theme: Customize Tailwind's default design system
  theme: {
    extend: {
      // Add custom theme extensions here without overriding defaults
      // Example:
      // colors: {
      //   primary: '#3b82f6',
      //   secondary: '#64748b',
      // },
      // fontFamily: {
      //   sans: ['Inter', 'ui-sans-serif', 'system-ui'],
      // },
    },
  },

  // Plugins: Extend Tailwind with additional utilities
  plugins: [
    // Add Tailwind plugins here
    // Example:
    // require('@tailwindcss/forms'),      // Better form defaults
    // require('@tailwindcss/typography'), // Prose styles
    // require('@tailwindcss/aspect-ratio'),
  ],
}
