// from https://github.com/ideal-postcodes/postcode
/**
 * @hidden
 */
export const DISTRICT_SPLIT_REGEX = /^([a-z]{1,2}\d)([a-z])$/i;
/**
 * Tests for the unit section of a postcode
 */
export const UNIT_REGEX = /[a-z]{2}$/i;
/**
 * Tests for the inward code section of a postcode
 */
export const INCODE_REGEX = /\d[a-z]{2}$/i;
/**
 * Tests for the outward code section of a postcode
 */
export const OUTCODE_REGEX = /^[a-z]{1,2}\d[a-z\d]?$/i;
/**
 * Tests for a valid postcode
 */
export const POSTCODE_REGEX = /^[a-z]{1,2}\d[a-z\d]?\s*\d[a-z]{2}$/i;
/**
 * Test for a valid postcode embedded in text
 */
export const POSTCODE_CORPUS_REGEX = /[a-z]{1,2}\d[a-z\d]?\s*\d[a-z]{2}/gi;
/**
 * Tests for the area section of a postcode
 */
export const AREA_REGEX = /^[a-z]{1,2}/i;
/**
 * Invalid postcode prototype
 * @hidden
 */
const invalidPostcode = {
    valid: false,
    postcode: null,
    incode: null,
    outcode: null,
    area: null,
    district: null,
    subDistrict: null,
    sector: null,
    unit: null,
};
/**
 * Return first elem of input is RegExpMatchArray or null if input null
 * @hidden
 */
const firstOrNull = (match) => {
    if (match === null)
        return null;
    return match[0];
};
const SPACE_REGEX = /\s+/gi;
/**
 * Drop all spaces and uppercase
 * @hidden
 */
const sanitize = (s) => s.replace(SPACE_REGEX, "").toUpperCase();
/**
 * Sanitizes string and returns regex matches
 * @hidden
 */
const matchOn = (s, regex) => sanitize(s).match(regex);
/**
 * Detects a "valid" postcode
 * - Starts and ends on a non-space character
 * - Any length of intervening space is allowed
 * - Must conform to one of following schemas:
 *  - AA1A 1AA
 *  - A1A 1AA
 *  - A1 1AA
 *  - A99 9AA
 *  - AA9 9AA
 *  - AA99 9AA
 */
export const isValid = (postcode) => postcode.match(POSTCODE_REGEX) !== null;
/**
 * Returns true if string is a valid outcode
 */
export const validOutcode = (outcode) => outcode.match(OUTCODE_REGEX) !== null;
/**
 * Returns a normalised postcode string (i.e. uppercased and properly spaced)
 *
 * Returns null if invalid postcode
 */
export const toNormalised = (postcode) => {
    const outcode = toOutcode(postcode);
    if (outcode === null)
        return null;
    const incode = toIncode(postcode);
    if (incode === null)
        return null;
    return `${outcode} ${incode}`;
};
/**
 * Returns a correctly formatted outcode given a postcode
 *
 * Returns null if invalid postcode
 */
export const toOutcode = (postcode) => {
    if (!isValid(postcode))
        return null;
    return sanitize(postcode).replace(INCODE_REGEX, "");
};
/**
 * Returns a correctly formatted incode given a postcode
 *
 * Returns null if invalid postcode
 */
export const toIncode = (postcode) => {
    if (!isValid(postcode))
        return null;
    const match = matchOn(postcode, INCODE_REGEX);
    return firstOrNull(match);
};
/**
 * Returns a correctly formatted area given a postcode
 *
 * Returns null if invalid postcode
 */
export const toArea = (postcode) => {
    if (!isValid(postcode))
        return null;
    const match = matchOn(postcode, AREA_REGEX);
    return firstOrNull(match);
};
/**
 * Returns a correctly formatted sector given a postcode
 *
 * Returns null if invalid postcode
 */
export const toSector = (postcode) => {
    const outcode = toOutcode(postcode);
    if (outcode === null)
        return null;
    const incode = toIncode(postcode);
    if (incode === null)
        return null;
    return `${outcode} ${incode[0]}`;
};
/**
 * Returns a correctly formatted unit given a postcode
 *
 * Returns null if invalid postcode
 */
export const toUnit = (postcode) => {
    if (!isValid(postcode))
        return null;
    const match = matchOn(postcode, UNIT_REGEX);
    return firstOrNull(match);
};
/**
 * Returns a correctly formatted district given a postcode
 *
 * Returns null if invalid postcode
 *
 * @example
 *
 * ```
 * toDistrict("AA9 9AA") // => "AA9"
 * toDistrict("A9A 9AA") // => "A9"
 * ```
 */
export const toDistrict = (postcode) => {
    const outcode = toOutcode(postcode);
    if (outcode === null)
        return null;
    const match = outcode.match(DISTRICT_SPLIT_REGEX);
    if (match === null)
        return outcode;
    return match[1];
};
/**
 * Returns a correctly formatted subdistrict given a postcode
 *
 * Returns null if no subdistrict is available on valid postcode
 * Returns null if invalid postcode
 *
 * @example
 *
 * ```
 * toSubDistrict("AA9A 9AA") // => "AA9A"
 * toSubDistrict("A9A 9AA") // => "A9A"
 * toSubDistrict("AA9 9AA") // => null
 * toSubDistrict("A9 9AA") // => null
 * ```
 */
export const toSubDistrict = (postcode) => {
    const outcode = toOutcode(postcode);
    if (outcode === null)
        return null;
    const split = outcode.match(DISTRICT_SPLIT_REGEX);
    if (split === null)
        return null;
    return outcode;
};
/**
 * Returns a ValidPostcode or InvalidPostcode object from a postcode string
 *
 * @example
 *
 * ```
 * import { parse } from "postcode";
 *
 * const {
 * postcode,    // => "SW1A 2AA"
 * outcode,     // => "SW1A"
 * incode,      // => "2AA"
 * area,        // => "SW"
 * district,    // => "SW1"
 * unit,        // => "AA"
 * sector,      // => "SW1A 2"
 * subDistrict, // => "SW1A"
 * valid,       // => true
 * } = parse("Sw1A     2aa");
 *
 * const {
 * postcode,    // => null
 * outcode,     // => null
 * incode,      // => null
 * area,        // => null
 * district,    // => null
 * unit,        // => null
 * sector,      // => null
 * subDistrict, // => null
 * valid,       // => false
 * } = parse("    Oh no, ):   ");
 * ```
 */
export const parse = (postcode) => {
    if (!isValid(postcode))
        return Object.assign({}, invalidPostcode);
    return {
        valid: true,
        postcode: toNormalised(postcode),
        incode: toIncode(postcode),
        outcode: toOutcode(postcode),
        area: toArea(postcode),
        district: toDistrict(postcode),
        subDistrict: toSubDistrict(postcode),
        sector: toSector(postcode),
        unit: toUnit(postcode),
    };
};
/**
 * Searches a body of text for postcode matches
 *
 * Returns an empty array if no match
 *
 * @example
 *
 * ```
 * // Retrieve valid postcodes in a body of text
 * const matches = match("The PM and her no.2 live at SW1A2aa and SW1A 2AB"); // => ["SW1A2aa", "SW1A 2AB"]
 *
 * // Perform transformations like normalisation postcodes using `.map` and `toNormalised`
 * matches.map(toNormalised); // => ["SW1A 2AA", "SW1A 2AB"]
 *
 * // No matches yields empty array
 * match("Some London outward codes are SW1A, NW1 and E1"); // => []
 * ```
 */
export const match = (corpus) => corpus.match(POSTCODE_CORPUS_REGEX) || [];
/**
 * Replaces postcodes in a body of text with a string
 *
 * By default the replacement string is empty string `""`
 *
 * @example
 *
 * ```
 * // Replace postcodes in a body of text
 * replace("The PM and her no.2 live at SW1A2AA and SW1A 2AB");
 * // => { match: ["SW1A2AA", "SW1A 2AB"], result: "The PM and her no.2 live at  and " }
 *
 * // Add custom replacement
 * replace("The PM lives at SW1A 2AA", "Downing Street");
 * // => { match: ["SW1A 2AA"], result: "The PM lives at Downing Street" };
 *
 * // No match
 * replace("Some London outward codes are SW1A, NW1 and E1");
 * // => { match: [], result: "Some London outward codes are SW1A, NW1 and E1" }
 * ```
 */
export const replace = (corpus, replaceWith = "") => ({
    match: match(corpus),
    result: corpus.replace(POSTCODE_CORPUS_REGEX, replaceWith),
});
export const FIXABLE_REGEX = /^\s*[a-z01]{1,2}[0-9oi][a-z\d]?\s*[0-9oi][a-z01]{2}\s*$/i;
/**
 * Attempts to fix and clean a postcode. Specifically:
 * - Performs character conversion on obviously wrong and commonly mixed up letters (e.g. O => 0 and vice versa)
 * - Trims string
 * - Properly adds space between outward and inward codes
 *
 * If the postcode cannot be coerced into a valid format, the original string is returned
 *
 * @example
 * ```javascript
 * fix(" SW1A  2AO") => "SW1A 2AO" // Properly spaces
 * fix("SW1A 2A0") => "SW1A 2AO" // 0 is coerced into "0"
 * ```
 *
 * Aims to be used in conjunction with parse to make postcode entry more forgiving:
 *
 * @example
 * ```javascript
 * const { inward } = parse(fix("SW1A 2A0")); // inward = "2AO"
 * ```
 */
export const fix = (s) => {
    const match = s.match(FIXABLE_REGEX);
    if (match === null)
        return s;
    s = s.toUpperCase().trim().replace(/\s+/gi, "");
    const l = s.length;
    const inward = s.slice(l - 3, l);
    return `${coerceOutcode(s.slice(0, l - 3))} ${coerce("NLL", inward)}`;
};
const toLetter = {
    "0": "O",
    "1": "I",
};
const toNumber = {
    O: "0",
    I: "1",
};
const coerceOutcode = (i) => {
    if (i.length === 2)
        return coerce("LN", i);
    if (i.length === 3)
        return coerce("L??", i);
    if (i.length === 4)
        return coerce("LLN?", i);
    return i;
};
/**
 * Given a pattern of letters, numbers and unknowns represented as a sequence
 * of L, Ns and ? respectively; coerce them into the correct type given a
 * mapping of potentially confused letters
 *
 * @hidden
 *
 * @example coerce("LLN", "0O8") => "OO8"
 */
const coerce = (pattern, input) => input
    .split("")
    .reduce((acc, c, i) => {
    const target = pattern.charAt(i);
    if (target === "N")
        acc.push(toNumber[c] || c);
    if (target === "L")
        acc.push(toLetter[c] || c);
    if (target === "?")
        acc.push(c);
    return acc;
}, [])
    .join("");

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImNvbXBvbmVudHMvcG9zdGNvZGUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUEsbURBQW1EO0FBRW5EOztHQUVHO0FBQ0gsTUFBTSxDQUFDLE1BQU0sb0JBQW9CLEdBQUcsMEJBQTBCLENBQUM7QUFDL0Q7O0dBRUc7QUFDSCxNQUFNLENBQUMsTUFBTSxVQUFVLEdBQUcsWUFBWSxDQUFDO0FBQ3ZDOztHQUVHO0FBQ0gsTUFBTSxDQUFDLE1BQU0sWUFBWSxHQUFHLGNBQWMsQ0FBQztBQUMzQzs7R0FFRztBQUNILE1BQU0sQ0FBQyxNQUFNLGFBQWEsR0FBRyx5QkFBeUIsQ0FBQztBQUN2RDs7R0FFRztBQUNILE1BQU0sQ0FBQyxNQUFNLGNBQWMsR0FBRyxzQ0FBc0MsQ0FBQztBQUVyRTs7R0FFRztBQUNILE1BQU0sQ0FBQyxNQUFNLHFCQUFxQixHQUFHLHFDQUFxQyxDQUFDO0FBRTNFOztHQUVHO0FBQ0gsTUFBTSxDQUFDLE1BQU0sVUFBVSxHQUFHLGNBQWMsQ0FBQztBQWdEekM7OztHQUdHO0FBQ0gsTUFBTSxlQUFlLEdBQW9CO0lBQ3JDLEtBQUssRUFBRSxLQUFLO0lBQ1osUUFBUSxFQUFFLElBQUk7SUFDZCxNQUFNLEVBQUUsSUFBSTtJQUNaLE9BQU8sRUFBRSxJQUFJO0lBQ2IsSUFBSSxFQUFFLElBQUk7SUFDVixRQUFRLEVBQUUsSUFBSTtJQUNkLFdBQVcsRUFBRSxJQUFJO0lBQ2pCLE1BQU0sRUFBRSxJQUFJO0lBQ1osSUFBSSxFQUFFLElBQUk7Q0FDYixDQUFDO0FBRUY7OztHQUdHO0FBQ0gsTUFBTSxXQUFXLEdBQUcsQ0FBQyxLQUE4QixFQUFpQixFQUFFO0lBQ2xFLElBQUksS0FBSyxLQUFLLElBQUk7UUFBRSxPQUFPLElBQUksQ0FBQztJQUNoQyxPQUFPLEtBQUssQ0FBQyxDQUFDLENBQUMsQ0FBQztBQUNwQixDQUFDLENBQUM7QUFFRixNQUFNLFdBQVcsR0FBRyxPQUFPLENBQUM7QUFFNUI7OztHQUdHO0FBQ0gsTUFBTSxRQUFRLEdB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RyxDQUFDLE9BQWUsRUFBRSxLQUFhLEVBQVUsRUFBRSxDQUN0RCxLQUFLO0tBQ0EsS0FBSyxDQUFDLEVBQUUsQ0FBQztLQUNULE1BQU0sQ0FBVyxDQUFDLEdBQUcsRUFBRSxDQUFDLEVBQUUsQ0FBQyxFQUFFLEVBQUU7SUFDNUIsTUFBTSxNQUFNLEdBQUcsT0FBTyxDQUFDLE1BQU0sQ0FBQyxDQUFDLENBQUMsQ0FBQztJQUNqQyxJQUFJLE1BQU0sS0FBSyxHQUFHO1FBQUUsR0FBRyxDQUFDLElBQUksQ0FBQyxRQUFRLENBQUMsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLENBQUM7SUFDL0MsSUFBSSxNQUFNLEtBQUssR0FBRztRQUFFLEdBQUcsQ0FBQyxJQUFJLENBQUMsUUFBUSxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsQ0FBQyxDQUFDO0lBQy9DLElBQUksTUFBTSxLQUFLLEdBQUc7UUFBRSxHQUFHLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQyxDQUFDO0lBQ2hDLE9BQU8sR0FBRyxDQUFDO0FBQ2YsQ0FBQyxFQUFFLEVBQUUsQ0FBQztLQUNMLElBQUksQ0FBQyxFQUFFLENBQUMsQ0FBQyIsImZpbGUiOiJjb21wb25lbnRzL3Bvc3Rjb2RlLmpzIiwic291cmNlc0NvbnRlbnQiOlsiLy8gZnJvbSBodHRwczovL2dpdGh1Yi5jb20vaWRlYWwtcG9zdGNvZGVzL3Bvc3Rjb2RlXG5cbi8qKlxuICogQGhpZGRlblxuICovXG5leHBvcnQgY29uc3QgRElTVFJJQ1RfU1BMSVRfUkVHRVggPSAvXihbYS16XXsxLDJ9XFxkKShbYS16XSkkL2k7XG4vKipcbiAqIFRlc3RzIGZvciB0aGUgdW5pdCBzZWN0aW9uIG9mIGEgcG9zdGNvZGVcbiAqL1xuZXhwb3J0IGNvbnN0IFVOSVRfUkVHRVggPSAvW2Etel17Mn0kL2k7XG4vKipcbiAqIFRlc3RzIGZvciB0aGUgaW53YXJkIGNvZGUgc2VjdGlvbiBvZiBhIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCBJTkNPREVfUkVHRVggPSAvXFxkW2Etel17Mn0kL2k7XG4vKipcbiAqIFRlc3RzIGZvciB0aGUgb3V0d2FyZCBjb2RlIHNlY3Rpb24gb2YgYSBwb3N0Y29kZVxuICovXG5leHBvcnQgY29uc3QgT1VUQ09ERV9SRUdFWCA9IC9eW2Etel17MSwyfVxcZFthLXpcXGRdPyQvaTtcbi8qKlxuICogVGVzdHMgZm9yIGEgdmFsaWQgcG9zdGNvZGVcbiAqL1xuZXhwb3J0IGNvbnN0IFBPU1RDT0RFX1JFR0VYID0gL15bYS16XXsxLDJ9XFxkW2EtelxcZF0/XFxzKlxcZFthLXpdezJ9JC9pO1xuXG4vKipcbiAqIFRlc3QgZm9yIGEgdmFsaWQgcG9zdGNvZGUgZW1iZWRkZWQgaW4gdGV4dFxuICovXG5leHBvcnQgY29uc3QgUE9TVENPREVfQ09SUFVTX1JFR0VYID0gL1thLXpdezEsMn1cXGRbYS16XFxkXT9cXHMqXFxkW2Etel17Mn0vZ2k7XG5cbi8qKlxuICogVGVzdHMgZm9yIHRoZSBhcmVhIHNlY3Rpb24gb2YgYSBwb3N0Y29kZVxuICovXG5leHBvcnQgY29uc3QgQVJFQV9SRUdFWCA9IC9eW2Etel17MSwyfS9pO1xuXG4vKipcbiAqIEBoaWRkZW5cbiAqL1xuaW50ZXJmYWNlIFZhbGlkYXRvciB7XG4gICAgKGlucHV0OiBzdHJpbmcpOiBib29sZWFuO1xufVxuXG4vKipcbiAqIEBoaWRkZW5cbiAqL1xuaW50ZXJmYWNlIFBhcnNlciB7XG4gICAgLyoqXG4gICAgICogQGhpZGRlblxuICAgICAqL1xuICAgIChwb3N0Y29kZTogc3RyaW5nKTogc3RyaW5nIHwgbnVsbDtcbn1cblxuLyoqXG4gKiBSZXByZXNlbnRzIGEgdmFsaWQgcG9zdGNvZGVcbiAqXG4gKiBOb3RlIHRoYXQgcmVzdWx0cyB3aWxsIGJlIG5vcm1hbGlzZWQgKGkuZS4gY29ycmVjdGx5IGZvcm1hdHRlZCksIGluY2x1ZGluZyBgcG9zdGNvZGVgXG4gKi9cbnR5cGUgVmFsaWRQb3N0Y29kZSA9IHtcbiAgICB2YWxpZDogdHJ1ZTtcbiAgICBwb3N0Y29kZTogc3RyaW5nO1xuICAgIGluY29kZTogc3RyaW5nO1xuICAgIG91dGNvZGU6IHN0cmluZztcbiAgICBhcmVhOiBzdHJpbmc7XG4gICAgZGlzdHJpY3Q6IHN0cmluZztcbiAgICBzdWJEaXN0cmljdDogc3RyaW5nIHwgbnVsbDtcbiAgICBzZWN0b3I6IHN0cmluZztcbiAgICB1bml0OiBzdHJpbmc7XG59O1xuXG50eXBlIEludmFsaWRQb3N0Y29kZSA9IHtcbiAgICB2YWxpZDogZmFsc2U7XG4gICAgcG9zdGNvZGU6IG51bGw7XG4gICAgaW5jb2RlOiBudWxsO1xuICAgIG91dGNvZGU6IG51bGw7XG4gICAgYXJlYTogbnVsbDtcbiAgICBkaXN0cmljdDogbnVsbDtcbiAgICBzdWJEaXN0cmljdDogbnVsbDtcbiAgICBzZWN0b3I6IG51bGw7XG4gICAgdW5pdDogbnVsbDtcbn07XG5cbi8qKlxuICogSW52YWxpZCBwb3N0Y29kZSBwcm90b3R5cGVcbiAqIEBoaWRkZW5cbiAqL1xuY29uc3QgaW52YWxpZFBvc3Rjb2RlOiBJbnZhbGlkUG9zdGNvZGUgPSB7XG4gICAgdmFsaWQ6IGZhbHNlLFxuICAgIHBvc3Rjb2RlOiBudWxsLFxuICAgIGluY29kZTogbnVsbCxcbiAgICBvdXRjb2RlOiBudWxsLFxuICAgIGFyZWE6IG51bGwsXG4gICAgZGlzdHJpY3Q6IG51bGwsXG4gICAgc3ViRGlzdHJpY3Q6IG51bGwsXG4gICAgc2VjdG9yOiBudWxsLFxuICAgIHVuaXQ6IG51bGwsXG59O1xuXG4vKipcbiAqIFJldHVybiBmaXJzdCBlbGVtIG9mIGlucHV0IGlzIFJlZ0V4cE1hdGNoQXJyYXkgb3IgbnVsbCBpZiBpbnB1dCBudWxsXG4gKiBAaGlkZGVuXG4gKi9cbmNvbnN0IGZpcnN0T3JOdWxsID0gKG1hdGNoOiBSZWdFeHBNYXRjaEFycmF5IHwgbnVsbCk6IHN0cmluZyB8IG51bGwgPT4ge1xuICAgIGlmIChtYXRjaCA9PT0gbnVsbCkgcmV0dXJuIG51bGw7XG4gICAgcmV0dXJuIG1hdGNoWzBdO1xufTtcblxuY29uc3QgU1BBQ0VfUkVHRVggPSAvXFxzKy9naTtcblxuLyoqXG4gKiBEcm9wIGFsbCBzcGFjZXMgYW5kIHVwcGVyY2FzZVxuICogQGhpZGRlblxuICovXG5jb25zdCBzYW5pdGl6ZSA9IChzOiBzdHJpbmcpOiBzdHJpbmcgPT5cbiAgICBzLnJlcGxhY2UoU1BBQ0VfUkVHRVgsIFwiXCIpLnRvVXBwZXJDYXNlKCk7XG5cbi8qKlxuICogU2FuaXRpemVzIHN0cmluZyBhbmQgcmV0dXJucyByZWdleCBtYXRjaGVzXG4gKiBAaGlkZGVuXG4gKi9cbmNvbnN0IG1hdGNoT24gPSAoczogc3RyaW5nLCByZWdleDogUmVnRXhwKTogUmVnRXhwTWF0Y2hBcnJheSB8IG51bGwgPT5cbiAgICBzYW5pdGl6ZShzKS5tYXRjaChyZWdleCk7XG5cbi8qKlxuICogRGV0ZWN0cyBhIFwidmFsaWRcIiBwb3N0Y29kZVxuICogLSBTdGFydHMgYW5kIGVuZHMgb24gYSBub24tc3BhY2UgY2hhcmFjdGVyXG4gKiAtIEFueSBsZW5ndGggb2YgaW50ZXJ2ZW5pbmcgc3BhY2UgaXMgYWxsb3dlZFxuICogLSBNdXN0IGNvbmZvcm0gdG8gb25lIG9mIGZvbGxvd2luZyBzY2hlbWFzOlxuICogIC0gQUExQSAxQUFcbiAqICAtIEExQSAxQUFcbiAqICAtIEExIDFBQVxuICogIC0gQTk5IDlBQVxuICogIC0gQUE5IDlBQVxuICogIC0gQUE5OSA5QUFcbiAqL1xuZXhwb3J0IGNvbnN0IGlzVmFsaWQ6IFZhbGlkYXRvciA9IChwb3N0Y29kZSkgPT5cbiAgICBwb3N0Y29kZS5tYXRjaChQT1NUQ09ERV9SRUdFWCkgIT09IG51bGw7XG5cbi8qKlxuICogUmV0dXJucyB0cnVlIGlmIHN0cmluZyBpcyBhIHZhbGlkIG91dGNvZGVcbiAqL1xuZXhwb3J0IGNvbnN0IHZhbGlkT3V0Y29kZTogVmFsaWRhdG9yID0gKG91dGNvZGUpID0+XG4gICAgb3V0Y29kZS5tYXRjaChPVVRDT0RFX1JFR0VYKSAhPT0gbnVsbDtcblxuLyoqXG4gKiBSZXR1cm5zIGEgbm9ybWFsaXNlZCBwb3N0Y29kZSBzdHJpbmcgKGkuZS4gdXBwZXJjYXNlZCBhbmQgcHJvcGVybHkgc3BhY2VkKVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCB0b05vcm1hbGlzZWQ6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGNvbnN0IG91dGNvZGUgPSB0b091dGNvZGUocG9zdGNvZGUpO1xuICAgIGlmIChvdXRjb2RlID09PSBudWxsKSByZXR1cm4gbnVsbDtcbiAgICBjb25zdCBpbmNvZGUgPSB0b0luY29kZShwb3N0Y29kZSk7XG4gICAgaWYgKGluY29kZSA9PT0gbnVsbCkgcmV0dXJuIG51bGw7XG4gICAgcmV0dXJuIGAke291dGNvZGV9ICR7aW5jb2RlfWA7XG59O1xuXG4vKipcbiAqIFJldHVybnMgYSBjb3JyZWN0bHkgZm9ybWF0dGVkIG91dGNvZGUgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCB0b091dGNvZGU6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGlmICghaXNWYWxpZChwb3N0Y29kZSkpIHJldHVybiBudWxsO1xuICAgIHJldHVybiBzYW5pdGl6ZShwb3N0Y29kZSkucmVwbGFjZShJTkNPREVfUkVHRVgsIFwiXCIpO1xufTtcblxuLyoqXG4gKiBSZXR1cm5zIGEgY29ycmVjdGx5IGZvcm1hdHRlZCBpbmNvZGUgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCB0b0luY29kZTogUGFyc2VyID0gKHBvc3Rjb2RlKSA9PiB7XG4gICAgaWYgKCFpc1ZhbGlkKHBvc3Rjb2RlKSkgcmV0dXJuIG51bGw7XG4gICAgY29uc3QgbWF0Y2ggPSBtYXRjaE9uKHBvc3Rjb2RlLCBJTkNPREVfUkVHRVgpO1xuICAgIHJldHVybiBmaXJzdE9yTnVsbChtYXRjaCk7XG59O1xuXG4vKipcbiAqIFJldHVybnMgYSBjb3JyZWN0bHkgZm9ybWF0dGVkIGFyZWEgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCB0b0FyZWE6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGlmICghaXNWYWxpZChwb3N0Y29kZSkpIHJldHVybiBudWxsO1xuICAgIGNvbnN0IG1hdGNoID0gbWF0Y2hPbihwb3N0Y29kZSwgQVJFQV9SRUdFWCk7XG4gICAgcmV0dXJuIGZpcnN0T3JOdWxsKG1hdGNoKTtcbn07XG5cbi8qKlxuICogUmV0dXJucyBhIGNvcnJlY3RseSBmb3JtYXR0ZWQgc2VjdG9yIGdpdmVuIGEgcG9zdGNvZGVcbiAqXG4gKiBSZXR1cm5zIG51bGwgaWYgaW52YWxpZCBwb3N0Y29kZVxuICovXG5leHBvcnQgY29uc3QgdG9TZWN0b3I6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGNvbnN0IG91dGNvZGUgPSB0b091dGNvZGUocG9zdGNvZGUpO1xuICAgIGlmIChvdXRjb2RlID09PSBudWxsKSByZXR1cm4gbnVsbDtcbiAgICBjb25zdCBpbmNvZGUgPSB0b0luY29kZShwb3N0Y29kZSk7XG4gICAgaWYgKGluY29kZSA9PT0gbnVsbCkgcmV0dXJuIG51bGw7XG4gICAgcmV0dXJuIGAke291dGNvZGV9ICR7aW5jb2RlWzBdfWA7XG59O1xuXG4vKipcbiAqIFJldHVybnMgYSBjb3JyZWN0bHkgZm9ybWF0dGVkIHVuaXQgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKi9cbmV4cG9ydCBjb25zdCB0b1VuaXQ6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGlmICghaXNWYWxpZChwb3N0Y29kZSkpIHJldHVybiBudWxsO1xuICAgIGNvbnN0IG1hdGNoID0gbWF0Y2hPbihwb3N0Y29kZSwgVU5JVF9SRUdFWCk7XG4gICAgcmV0dXJuIGZpcnN0T3JOdWxsKG1hdGNoKTtcbn07XG5cbi8qKlxuICogUmV0dXJucyBhIGNvcnJlY3RseSBmb3JtYXR0ZWQgZGlzdHJpY3QgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKlxuICogQGV4YW1wbGVcbiAqXG4gKiBgYGBcbiAqIHRvRGlzdHJpY3QoXCJBQTkgOUFBXCIpIC8vID0+IFwiQUE5XCJcbiAqIHRvRGlzdHJpY3QoXCJBOUEgOUFBXCIpIC8vID0+IFwiQTlcIlxuICogYGBgXG4gKi9cbmV4cG9ydCBjb25zdCB0b0Rpc3RyaWN0OiBQYXJzZXIgPSAocG9zdGNvZGUpID0+IHtcbiAgICBjb25zdCBvdXRjb2RlID0gdG9PdXRjb2RlKHBvc3Rjb2RlKTtcbiAgICBpZiAob3V0Y29kZSA9PT0gbnVsbCkgcmV0dXJuIG51bGw7XG4gICAgY29uc3QgbWF0Y2ggPSBvdXRjb2RlLm1hdGNoKERJU1RSSUNUX1NQTElUX1JFR0VYKTtcbiAgICBpZiAobWF0Y2ggPT09IG51bGwpIHJldHVybiBvdXRjb2RlO1xuICAgIHJldHVybiBtYXRjaFsxXTtcbn07XG5cbi8qKlxuICogUmV0dXJucyBhIGNvcnJlY3RseSBmb3JtYXR0ZWQgc3ViZGlzdHJpY3QgZ2l2ZW4gYSBwb3N0Y29kZVxuICpcbiAqIFJldHVybnMgbnVsbCBpZiBubyBzdWJkaXN0cmljdCBpcyBhdmFpbGFibGUgb24gdmFsaWQgcG9zdGNvZGVcbiAqIFJldHVybnMgbnVsbCBpZiBpbnZhbGlkIHBvc3Rjb2RlXG4gKlxuICogQGV4YW1wbGVcbiAqXG4gKiBgYGBcbiAqIHRvU3ViRGlzdHJpY3QoXCJBQTlBIDlBQVwiKSAvLyA9PiBcIkFBOUFcIlxuICogdG9TdWJEaXN0cmljdChcIkE5QSA5QUFcIikgLy8gPT4gXCJBOUFcIlxuICogdG9TdWJEaXN0cmljdChcIkFBOSA5QUFcIikgLy8gPT4gbnVsbFxuICogdG9TdWJEaXN0cmljdChcIkE5IDlBQVwiKSAvLyA9PiBudWxsXG4gKiBgYGBcbiAqL1xuZXhwb3J0IGNvbnN0IHRvU3ViRGlzdHJpY3Q6IFBhcnNlciA9IChwb3N0Y29kZSkgPT4ge1xuICAgIGNvbnN0IG91dGNvZGUgPSB0b091dGNvZGUocG9zdGNvZGUpO1xuICAgIGlmIChvdXRjb2RlID09PSBudWxsKSByZXR1cm4gbnVsbDtcbiAgICBjb25zdCBzcGxpdCA9IG91dGNvZGUubWF0Y2goRElTVFJJQ1RfU1BMSVRfUkVHRVgpO1xuICAgIGlmIChzcGxpdCA9PT0gbnVsbCkgcmV0dXJuIG51bGw7XG4gICAgcmV0dXJuIG91dGNvZGU7XG59O1xuXG4vKipcbiAqIFJldHVybnMgYSBWYWxpZFBvc3Rjb2RlIG9yIEludmFsaWRQb3N0Y29kZSBvYmplY3QgZnJvbSBhIHBvc3Rjb2RlIHN0cmluZ1xuICpcbiAqIEBleGFtcGxlXG4gKlxuICogYGBgXG4gKiBpbXBvcnQgeyBwYXJzZSB9IGZyb20gXCJwb3N0Y29kZVwiO1xuICpcbiAqIGNvbnN0IHtcbiAqIHBvc3Rjb2RlLCAgICAvLyA9PiBcIlNXMUEgMkFBXCJcbiAqIG91dGNvZGUsICAgICAvLyA9PiBcIlNXMUFcIlxuICogaW5jb2RlLCAgICAgIC8vID0+IFwiMkFBXCJcbiAqIGFyZWEsICAgICAgICAvLyA9PiBcIlNXXCJcbiAqIGRpc3RyaWN0LCAgICAvLyA9PiBcIlNXMVwiXG4gKiB1bml0LCAgICAgICAgLy8gPT4gXCJBQVwiXG4gKiBzZWN0b3IsICAgICAgLy8gPT4gXCJTVzFBIDJcIlxuICogc3ViRGlzdHJpY3QsIC8vID0+IFwiU1cxQVwiXG4gKiB2YWxpZCwgICAgICAgLy8gPT4gdHJ1ZVxuICogfSA9IHBhcnNlKFwiU3cxQSAgICAgMmFhXCIpO1xuICpcbiAqIGNvbnN0IHtcbiAqIHBvc3Rjb2RlLCAgICAvLyA9PiBudWxsXG4gKiBvdXRjb2RlLCAgICAgLy8gPT4gbnVsbFxuICogaW5jb2RlLCAgICAgIC8vID0+IG51bGxcbiAqIGFyZWEsICAgICAgICAvLyA9PiBudWxsXG4gKiBkaXN0cmljdCwgICAgLy8gPT4gbnVsbFxuICogdW5pdCwgICAgICAgIC8vID0+IG51bGxcbiAqIHNlY3RvciwgICAgICAvLyA9PiBudWxsXG4gKiBzdWJEaXN0cmljdCwgLy8gPT4gbnVsbFxuICogdmFsaWQsICAgICAgIC8vID0+IGZhbHNlXG4gKiB9ID0gcGFyc2UoXCIgICAgT2ggbm8sICk6ICAgXCIpO1xuICogYGBgXG4gKi9cbmV4cG9ydCBjb25zdCBwYXJzZSA9IChwb3N0Y29kZTogc3RyaW5nKTogVmFsaWRQb3N0Y29kZSB8IEludmFsaWRQb3N0Y29kZSA9PiB7XG4gICAgaWYgKCFpc1ZhbGlkKHBvc3Rjb2RlKSkgcmV0dXJuIHsgLi4uaW52YWxpZFBvc3Rjb2RlIH07XG4gICAgcmV0dXJuIHtcbiAgICAgICAgdmFsaWQ6IHRydWUsXG4gICAgICAgIHBvc3Rjb2RlOiB0b05vcm1hbGlzZWQocG9zdGNvZGUpIGFzIHN0cmluZyxcbiAgICAgICAgaW5jb2RlOiB0b0luY29kZShwb3N0Y29kZSkgYXMgc3RyaW5nLFxuICAgICAgICBvdXRjb2RlOiB0b091dGNvZGUocG9zdGNvZGUpIGFzIHN0cmluZyxcbiAgICAgICAgYXJlYTogdG9BcmVhKHBvc3Rjb2RlKSBhcyBzdHJpbmcsXG4gICAgICAgIGRpc3RyaWN0OiB0b0Rpc3RyaWN0KHBvc3Rjb2RlKSBhcyBzdHJpbmcsXG4gICAgICAgIHN1YkRpc3RyaWN0OiB0b1N1YkRpc3RyaWN0KHBvc3Rjb2RlKSxcbiAgICAgICAgc2VjdG9yOiB0b1NlY3Rvcihwb3N0Y29kZSkgYXMgc3RyaW5nLFxuICAgICAgICB1bml0OiB0b1VuaXQocG9zdGNvZGUpIGFzIHN0cmluZyxcbiAgICB9O1xufTtcblxuLyoqXG4gKiBTZWFyY2hlcyBhIGJvZHkgb2YgdGV4dCBmb3IgcG9zdGNvZGUgbWF0Y2hlc1xuICpcbiAqIFJldHVybnMgYW4gZW1wdHkgYXJyYXkgaWYgbm8gbWF0Y2hcbiAqXG4gKiBAZXhhbXBsZVxuICpcbiAqIGBgYFxuICogLy8gUmV0cmlldmUgdmFsaWQgcG9zdGNvZGVzIGluIGEgYm9keSBvZiB0ZXh0XG4gKiBjb25zdCBtYXRjaGVzID0gbWF0Y2goXCJUaGUgUE0gYW5kIGhlciBuby4yIGxpdmUgYXQgU1cxQTJhYSBhbmQgU1cxQSAyQUJcIik7IC8vID0+IFtcIlNXMUEyYWFcIiwgXCJTVzFBIDJBQlwiXVxuICpcbiAqIC8vIFBlcmZvcm0gdHJhbnNmb3JtYXRpb25zIGxpa2Ugbm9ybWFsaXNhdGlvbiBwb3N0Y29kZXMgdXNpbmcgYC5tYXBgIGFuZCBgdG9Ob3JtYWxpc2VkYFxuICogbWF0Y2hlcy5tYXAodG9Ob3JtYWxpc2VkKTsgLy8gPT4gW1wiU1cxQSAyQUFcIiwgXCJTVzFBIDJBQlwiXVxuICpcbiAqIC8vIE5vIG1hdGNoZXMgeWllbGRzIGVtcHR5IGFycmF5XG4gKiBtYXRjaChcIlNvbWUgTG9uZG9uIG91dHdhcmQgY29kZXMgYXJlIFNXMUEsIE5XMSBhbmQgRTFcIik7IC8vID0+IFtdXG4gKiBgYGBcbiAqL1xuZXhwb3J0IGNvbnN0IG1hdGNoID0gKGNvcnB1czogc3RyaW5nKTogc3RyaW5nW10gPT5cbiAgICBjb3JwdXMubWF0Y2goUE9TVENPREVfQ09SUFVTX1JFR0VYKSB8fCBbXTtcblxuLyoqXG4gKiBAaGlkZGVuXG4gKi9cbmludGVyZmFjZSBSZXBsYWNlUmVzdWx0IHtcbiAgICAvKipcbiAgICAgKiBMaXN0IG9mIG1hdGNoaW5nIHBvc3Rjb2RlcyBmb3VuZCBpbnRleHRcbiAgICAgKi9cbiAgICBtYXRjaDogc3RyaW5nW107XG4gICAgLyoqXG4gICAgICogQm9keSBvZiB0ZXh0IHdpdGggcG9zdGNvZGVzIHJlcGxhY2VkICh3aXRoIGVtcHR5IHN0cmluZyBieSBkZWZhdWx0KVxuICAgICAqL1xuICAgIHJlc3VsdDogc3RyaW5nO1xufVxuXG4vKipcbiAqIFJlcGxhY2VzIHBvc3Rjb2RlcyBpbiBhIGJvZHkgb2YgdGV4dCB3aXRoIGEgc3RyaW5nXG4gKlxuICogQnkgZGVmYXVsdCB0aGUgcmVwbGFjZW1lbnQgc3RyaW5nIGlzIGVtcHR5IHN0cmluZyBgXCJcImBcbiAqXG4gKiBAZXhhbXBsZVxuICpcbiAqIGBgYFxuICogLy8gUmVwbGFjZSBwb3N0Y29kZXMgaW4gYSBib2R5IG9mIHRleHRcbiAqIHJlcGxhY2UoXCJUaGUgUE0gYW5kIGhlciBuby4yIGxpdmUgYXQgU1cxQTJBQSBhbmQgU1cxQSAyQUJcIik7XG4gKiAvLyA9PiB7IG1hdGNoOiBbXCJTVzFBMkFBXCIsIFwiU1cxQSAyQUJcIl0sIHJlc3VsdDogXCJUaGUgUE0gYW5kIGhlciBuby4yIGxpdmUgYXQgIGFuZCBcIiB9XG4gKlxuICogLy8gQWRkIGN1c3RvbSByZXBsYWNlbWVudFxuICogcmVwbGFjZShcIlRoZSBQTSBsaXZlcyBhdCBTVzFBIDJBQVwiLCBcIkRvd25pbmcgU3RyZWV0XCIpO1xuICogLy8gPT4geyBtYXRjaDogW1wiU1cxQSAyQUFcIl0sIHJlc3VsdDogXCJUaGUgUE0gbGl2ZXMgYXQgRG93bmluZyBTdHJlZXRcIiB9O1xuICpcbiAqIC8vIE5vIG1hdGNoXG4gKiByZXBsYWNlKFwiU29tZSBMb25kb24gb3V0d2FyZCBjb2RlcyBhcmUgU1cxQSwgTlcxIGFuZCBFMVwiKTtcbiAqIC8vID0+IHsgbWF0Y2g6IFtdLCByZXN1bHQ6IFwiU29tZSBMb25kb24gb3V0d2FyZCBjb2RlcyBhcmUgU1cxQSwgTlcxIGFuZCBFMVwiIH1cbiAqIGBgYFxuICovXG5leHBvcnQgY29uc3QgcmVwbGFjZSA9IChjb3JwdXM6IHN0cmluZywgcmVwbGFjZVdpdGggPSBcIlwiKTogUmVwbGFjZVJlc3VsdCA9PiAoe1xuICAgIG1hdGNoOiBtYXRjaChjb3JwdXMpLFxuICAgIHJlc3VsdDogY29ycHVzLnJlcGxhY2UoUE9TVENPREVfQ09SUFVTX1JFR0VYLCByZXBsYWNlV2l0aCksXG59KTtcblxuZXhwb3J0IGNvbnN0IEZJWEFCTEVfUkVHRVggPSAvXlxccypbYS16MDFdezEsMn1bMC05b2ldW2EtelxcZF0/XFxzKlswLTlvaV1bYS16MDFdezJ9XFxzKiQvaTtcblxuLyoqXG4gKiBBdHRlbXB0cyB0byBmaXggYW5kIGNsZWFuIGEgcG9zdGNvZGUuIFNwZWNpZmljYWxseTpcbiAqIC0gUGVyZm9ybXMgY2hhcmFjdGVyIGNvbnZlcnNpb24gb24gb2J2aW91c2x5IHdyb25nIGFuZCBjb21tb25seSBtaXhlZCB1cCBsZXR0ZXJzIChlLmcuIE8gPT4gMCBhbmQgdmljZSB2ZXJzYSlcbiAqIC0gVHJpbXMgc3RyaW5nXG4gKiAtIFByb3Blcmx5IGFkZHMgc3BhY2UgYmV0d2VlbiBvdXR3YXJkIGFuZCBpbndhcmQgY29kZXNcbiAqXG4gKiBJZiB0aGUgcG9zdGNvZGUgY2Fubm90IGJlIGNvZXJjZWQgaW50byBhIHZhbGlkIGZvcm1hdCwgdGhlIG9yaWdpbmFsIHN0cmluZyBpcyByZXR1cm5lZFxuICpcbiAqIEBleGFtcGxlXG4gKiBgYGBqYXZhc2NyaXB0XG4gKiBmaXgoXCIgU1cxQSAgMkFPXCIpID0+IFwiU1cxQSAyQU9cIiAvLyBQcm9wZXJseSBzcGFjZXNcbiAqIGZpeChcIlNXMUEgMkEwXCIpID0+IFwiU1cxQSAyQU9cIiAvLyAwIGlzIGNvZXJjZWQgaW50byBcIjBcIlxuICogYGBgXG4gKlxuICogQWltcyB0byBiZSB1c2VkIGluIGNvbmp1bmN0aW9uIHdpdGggcGFyc2UgdG8gbWFrZSBwb3N0Y29kZSBlbnRyeSBtb3JlIGZvcmdpdmluZzpcbiAqXG4gKiBAZXhhbXBsZVxuICogYGBgamF2YXNjcmlwdFxuICogY29uc3QgeyBpbndhcmQgfSA9IHBhcnNlKGZpeChcIlNXMUEgMkEwXCIpKTsgLy8gaW53YXJkID0gXCIyQU9cIlxuICogYGBgXG4gKi9cbmV4cG9ydCBjb25zdCBmaXggPSAoczogc3RyaW5nKTogc3RyaW5nID0+IHtcbiAgICBjb25zdCBtYXRjaCA9IHMubWF0Y2goRklYQUJMRV9SRUdFWCk7XG4gICAgaWYgKG1hdGNoID09PSBudWxsKSByZXR1cm4gcztcbiAgICBzID0gcy50b1VwcGVyQ2FzZSgpLnRyaW0oKS5yZXBsYWNlKC9cXHMrL2dpLCBcIlwiKTtcbiAgICBjb25zdCBsID0gcy5sZW5ndGg7XG4gICAgY29uc3QgaW53YXJkID0gcy5zbGljZShsIC0gMywgbCk7XG4gICAgcmV0dXJuIGAke2NvZXJjZU91dGNvZGUocy5zbGljZSgwLCBsIC0gMykpfSAke2NvZXJjZShcIk5MTFwiLCBpbndhcmQpfWA7XG59O1xuXG5jb25zdCB0b0xldHRlcjogUmVjb3JkPHN0cmluZywgc3RyaW5nPiA9IHtcbiAgICBcIjBcIjogXCJPXCIsXG4gICAgXCIxXCI6IFwiSVwiLFxufTtcblxuY29uc3QgdG9OdW1iZXI6IFJlY29yZDxzdHJpbmcsIHN0cmluZz4gPSB7XG4gICAgTzogXCIwXCIsXG4gICAgSTogXCIxXCIsXG59O1xuXG5jb25zdCBjb2VyY2VPdXRjb2RlID0gKGk6IHN0cmluZyk6IHN0cmluZyA9PiB7XG4gICAgaWYgKGkubGVuZ3RoID09PSAyKSByZXR1cm4gY29lcmNlKFwiTE5cIiwgaSk7XG4gICAgaWYgKGkubGVuZ3RoID09PSAzKSByZXR1cm4gY29lcmNlKFwiTD8/XCIsIGkpO1xuICAgIGlmIChpLmxlbmd0aCA9PT0gNCkgcmV0dXJuIGNvZXJjZShcIkxMTj9cIiwgaSk7XG4gICAgcmV0dXJuIGk7XG59O1xuXG4vKipcbiAqIEdpdmVuIGEgcGF0dGVybiBvZiBsZXR0ZXJzLCBudW1iZXJzIGFuZCB1bmtub3ducyByZXByZXNlbnRlZCBhcyBhIHNlcXVlbmNlXG4gKiBvZiBMLCBOcyBhbmQgPyByZXNwZWN0aXZlbHk7IGNvZXJjZSB0aGVtIGludG8gdGhlIGNvcnJlY3QgdHlwZSBnaXZlbiBhXG4gKiBtYXBwaW5nIG9mIHBvdGVudGlhbGx5IGNvbmZ1c2VkIGxldHRlcnNcbiAqXG4gKiBAaGlkZGVuXG4gKlxuICogQGV4YW1wbGUgY29lcmNlKFwiTExOXCIsIFwiME84XCIpID0+IFwiT084XCJcbiAqL1xuY29uc3QgY29lcmNlID0gKHBhdHRlcm46IHN0cmluZywgaW5wdXQ6IHN0cmluZyk6IHN0cmluZyA9PlxuICAgIGlucHV0XG4gICAgICAgIC5zcGxpdChcIlwiKVxuICAgICAgICAucmVkdWNlPHN0cmluZ1tdPigoYWNjLCBjLCBpKSA9PiB7XG4gICAgICAgICAgICBjb25zdCB0YXJnZXQgPSBwYXR0ZXJuLmNoYXJBdChpKTtcbiAgICAgICAgICAgIGlmICh0YXJnZXQgPT09IFwiTlwiKSBhY2MucHVzaCh0b051bWJlcltjXSB8fCBjKTtcbiAgICAgICAgICAgIGlmICh0YXJnZXQgPT09IFwiTFwiKSBhY2MucHVzaCh0b0xldHRlcltjXSB8fCBjKTtcbiAgICAgICAgICAgIGlmICh0YXJnZXQgPT09IFwiP1wiKSBhY2MucHVzaChjKTtcbiAgICAgICAgICAgIHJldHVybiBhY2M7XG4gICAgICAgIH0sIFtdKVxuICAgICAgICAuam9pbihcIlwiKTsiXX0=
