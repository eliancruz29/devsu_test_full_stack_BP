import { ErrorResponse } from "../models/error.response";

export function isErrorResponse(obj: any): obj is ErrorResponse {
  return (
    obj !== null &&
    typeof obj === 'object' &&
    typeof obj.code === 'string' &&
    typeof obj.message === 'string'
  );
}
