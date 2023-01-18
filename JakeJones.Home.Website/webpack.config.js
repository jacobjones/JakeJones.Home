const path = require("path");
const fs = require("fs");
const postcssPresetEnv = require("postcss-preset-env");
const neat = require('node-neat');

const { minify } = require("terser");

const MergeIntoSingleFilePlugin = require("webpack-merge-and-include-globally");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const isDev = process.env.NODE_ENV !== "production";

const scssFiles = fs.readdirSync("./Content/sass").filter(function (file) {
  return file.match(/.*\.scss$/);
}).map(file => `./Content/sass/${file}`);

module.exports = {
  mode: isDev ? "development" : "production",
  entry: scssFiles,
  output: {
    path: path.resolve(__dirname, "wwwroot"),
    filename: "js/sass.js",
  },
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          { loader: MiniCssExtractPlugin.loader },
          { loader: "css-loader", options: { sourceMap: isDev, importLoaders: 2 } },
          { loader: "postcss-loader", options: { postcssOptions: { plugins: isDev ? () => [] : () => [postcssPresetEnv({ browsers: [">1%"], }), require("cssnano")()] } } },
          { loader: "sass-loader", options: { sourceMap: isDev, sassOptions: { includePaths: neat.includePaths } } },
        ],
      },
      {
        test: /\.(woff2?|eot)$/,
        type: 'asset/resource',
        generator: {
          filename: 'fonts/[name][ext]',
        },
      },
    ],
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: isDev ? "css/all.css" : "css/all.min.css",
    }),
    new MergeIntoSingleFilePlugin({
      files: { "js/all.js": [ "./Content/js/*.js" ] },
      transformFileName: (fileNameBase, extension) => isDev ? `${fileNameBase}${extension}` : `${fileNameBase}.min${extension}`,
      transform: {
        'js/all.min.js': async (code) => (await minify(code)).code
      },
    }),
  ]
};
