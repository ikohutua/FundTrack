import { Injectable } from "@angular/core";
import { FormControl } from "@angular/forms";

@Injectable()
export class ValidatorsService {

   public isInteger = (control:FormControl) => {
        return check_if_is_integer(control.value) ? null : {
           notNumeric: true
        }
   }

}

function check_if_is_integer(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}