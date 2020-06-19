const IS_DEVELOPMENT = process.env.NODE_ENV === 'development';


const plugins = [
    require('tailwindcss'),
    require('postcss-import'),
    require('tailwindcss'),
    require('autoprefixer')
];


if(!IS_DEVELOPMENT) {
    const purgecss = require('@fullhuman/postcss-purgecss')({
        content: [
          './src/**/*.html'
        ],
      
        // This is the function used to extract class names from your templates
        defaultExtractor: content => {
          // Capture as liberally as possible, including things like `h-(screen-1.5)`
          const broadMatches = content.match(/[^<>"'`\s]*[^<>"'`\s:]/g) || []
      
          // Capture classes within other delimiters like .block(class="w-1/2") in Pug
          const innerMatches = content.match(/[^<>"'`\s.()]*[^<>"'`\s.():]/g) || []
      
          return broadMatches.concat(innerMatches)
        }
    });
    plugins.push(purgecss);
}

module.exports = {
    plugins: plugins
}