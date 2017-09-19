import { Injectable } from "@angular/core";
import { FormControl } from "@angular/forms";

@Injectable()
export class ValidatorsService {

   public isInteger = (control:FormControl) => {
        return this.check_if_is_integer(control.value) ? null : {
           notNumeric: true
        }
    }

    public isNumber = (control: FormControl) => {
        return this.check_if_is_number(control.value) ? null : {
            notNumber: true
        }
   }

    public isMinValue = (control: FormControl) => {
        return this.check_if_min_value(control.value) ? null : {
            notMinValue: true
        }
    }

    public isMaxValue = (control: FormControl) => {
        return this.check_if_max_value(control.value) ? null : {
            notMaxValue: true
        }
    }

    check_if_min_value(value) {
    if (value > 0) {
        return true;
    } else {
        return false;
    }
}

    check_if_max_value(value) {
    if (value < 1000000) {
        return true;
    } else {
        return false;
    }
} 

    check_if_is_number(value) {
    if (!isNaN(value)) {
        return true;
    } else {
        return false;
    }
}

    check_if_is_integer(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}

}