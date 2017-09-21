import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'truncate'
})
//Cutting string 
export class TruncatePipe implements PipeTransform {
    /**
     * Cutting string
     * @param value
     * @param length
     * @returns Cutted string
     */
    transform(value: string, length: number): string {
        return value.length > length ? value.substring(0, length) + ' ...' : value;
    }
}