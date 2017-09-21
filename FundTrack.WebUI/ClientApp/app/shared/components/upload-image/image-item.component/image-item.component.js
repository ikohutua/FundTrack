"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var image_1 = require('../shared/image');
var ImageItemComponent = (function () {
    function ImageItemComponent() {
        this.delete = new core_1.EventEmitter();
    }
    ImageItemComponent.prototype.onDelete = function () {
        this.delete.emit(this.image);
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', image_1.Image)
    ], ImageItemComponent.prototype, "image", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', core_1.EventEmitter)
    ], ImageItemComponent.prototype, "delete", void 0);
    ImageItemComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'image-item',
            templateUrl: 'image-item.component.html',
            styleUrls: ['image-item.component.css']
        }), 
        __metadata('design:paramtypes', [])
    ], ImageItemComponent);
    return ImageItemComponent;
}());
exports.ImageItemComponent = ImageItemComponent;
//# sourceMappingURL=image-item.component.js.map