/** for display new events */
export interface IEventModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    date: Date;
    pathToCoverImage: string;
}