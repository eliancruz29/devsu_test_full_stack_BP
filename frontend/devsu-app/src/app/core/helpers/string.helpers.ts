export function isNullOrUndefinedOrEmpty(value?: string): boolean {
  return (
    value === null ||
    value === undefined ||
    value === '');
}
