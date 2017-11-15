export class ImportIntervalViewModel
{
    constructor(name: string, minutes: number) {
        this.minutes = minutes;
        this.name = name;
    }
    public name: string;
    public minutes: number;
}