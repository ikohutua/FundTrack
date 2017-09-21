import Targetviewmodel = require("./target.view-model");
import TargetViewModel = Targetviewmodel.TargetViewModel;

export class ParentTargetViewModel {
    targetId: number;
    name: string;
    organizationId: number;
    subTargets: TargetViewModel[];
}