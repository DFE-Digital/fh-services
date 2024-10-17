import '@dotenvx/dotenvx'

/**
 * Generates a test ID starting from the base ID configured in the environment variables.
 * @param {number} id The normalised ID to be added onto the base ID (e.g. 1 -> 1,000,001)
 */
export const testId = (id) => parseInt(process.env.IDS_START_FROM) + id

/**
 * Prefixes any given string with an E2E prefix
 * @param {string} text
 */
export const testPrefix = (text) => `[E2E] ${text}`
