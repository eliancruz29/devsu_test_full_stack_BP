import { EnumObject } from "../../core/enums/enum-object";
import { Gender } from "../../core/enums/gender";

export function getGenderAsEnumObjectOptions(): EnumObject<number>[] {
  return [
    { value: 1, label: Gender.Male },
    { value: 2, label: Gender.Female },
    { value: 3, label: Gender.NotSpecified }
  ];
}