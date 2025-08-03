import { ErrorResponse } from "../models/error.response";

export function isErrorResponse(obj: any): obj is ErrorResponse {
  return (
    obj !== null &&
    typeof obj === 'object' &&
    typeof obj.code === 'string' &&
    typeof obj.message === 'string'
  );
}

export function areAllPropertiesFulfilled<T extends Record<string, unknown>>(obj: T): boolean {
  return Object.values(obj).every(value => value !== null && value !== undefined && value !== '');
}

