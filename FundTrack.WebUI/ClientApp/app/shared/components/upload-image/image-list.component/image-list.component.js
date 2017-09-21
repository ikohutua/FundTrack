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
var ImageListComponent = (function () {
    function ImageListComponent() {
        this.images = [];
        this.getImageChange = new core_1.EventEmitter();
        this.activeColor = 'green';
        this.baseColor = '#ccc';
        this.overlayColor = 'rgba(255,255,255,0.5)';
        this.dragging = false;
        this.loaded = false;
        this.imageLoaded = false;
        this.imageSrc = '';
    }
    ImageListComponent.prototype.delete = function (img) {
        var index = this.images.indexOf(img);
        if (index > -1)
            this.images.splice(index, 1);
    };
    ImageListComponent.prototype.handleDragEnter = function () {
        this.dragging = true;
    };
    ImageListComponent.prototype.handleDragLeave = function () {
        this.dragging = false;
    };
    ImageListComponent.prototype.handleDrop = function (e) {
        e.preventDefault();
        this.dragging = false;
        this.handleInputChange(e);
    };
    ImageListComponent.prototype.handleImageLoad = function () {
        this.imageLoaded = true;
    };
    ImageListComponent.prototype.handleInputChange = function (e) {
        var _this = this;
        this.currentFile = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
        var pattern = /image-*/;
        var reader = new FileReader();
        if (!this.currentFile.type.match(pattern)) {
            alert('Невірний формат!');
            return;
        }
        if (this.images.find(function (i) { return i.name == _this.currentFile.name; }) != null) {
            alert('Зображення вже вибране!');
            return;
        }
        this.loaded = false;
        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsDataURL(this.currentFile);
    };
    ImageListComponent.prototype._handleReaderLoaded = function (e) {
        var reader = e.target;
        this.imageSrc = reader.result;
        var commaInd = this.imageSrc.indexOf(',');
        var byteCharacters = this.imageSrc.substring(commaInd + 1);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        var item = new image_1.Image(this.currentFile.name, this.imageSrc, byteArray.toString());
        this.images.push(item);
        this.getImageChange.emit(this.images);
        this.loaded = true;
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', Array)
    ], ImageListComponent.prototype, "images", void 0);
    __decorate([
        core_1.Output(), 
        __metadata('design:type', Object)
    ], ImageListComponent.prototype, "getImageChange", void 0);
    ImageListComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'image-list',
            templateUrl: 'image-list.component.html',
            styleUrls: ['image-list.component.css'],
            inputs: ['activeColor', 'baseColor', 'overlayColor']
        }), 
        __metadata('design:paramtypes', [])
    ], ImageListComponent);
    return ImageListComponent;
}());
exports.ImageListComponent = ImageListComponent;
//# sourceMappingURL=image-list.component.js.map