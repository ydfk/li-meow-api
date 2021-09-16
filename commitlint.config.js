/*
 * @Description: Copyright (c) ydfk. All rights reserved
 * @Author: ydfk
 * @Date: 2021-09-16 14:48:25
 * @LastEditors: ydfk
 * @LastEditTime: 2021-09-16 14:48:26
 */
module.exports = {
  ignores: [(commit) => commit.includes("init")],
  extends: ["@commitlint/config-conventional"],
  rules: {
    "body-leading-blank": [2, "always"],
    "footer-leading-blank": [1, "always"],
    "header-max-length": [2, "always", 108],
    "subject-empty": [2, "never"],
    "type-empty": [2, "never"],
    "type-enum": [
      2,
      "always",
      ["feat", "fix", "perf", "style", "docs", "test", "refactor", "build", "ci", "chore", "revert", "wip", "workflow", "types", "release"],
    ],
  },
};
