import { Question } from "./question.model";
import { EditorComponent } from "../shared";
import {  QuestionDelete, QuestionEdit, QuestionAdd } from "./question.actions";

const template = require("./question-edit-embed.component.html");
const styles = require("./question-edit-embed.component.scss");

export class QuestionEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "question",
            "question-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.question ? "Edit Question": "Create Question";

        if (this.question) {                
            this._nameInputElement.value = this.question.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const question = {
            id: this.question != null ? this.question.id : null,
            name: this._nameInputElement.value
        } as Question;
        
        this.dispatchEvent(new QuestionAdd(question));            
    }

    public onDelete() {        
        const question = {
            id: this.question != null ? this.question.id : null,
            name: this._nameInputElement.value
        } as Question;

        this.dispatchEvent(new QuestionDelete(question));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "question-id":
                this.questionId = newValue;
                break;
            case "question":
                this.question = JSON.parse(newValue);
                if (this.parentNode) {
                    this.questionId = this.question.id;
                    this._nameInputElement.value = this.question.name != undefined ? this.question.name : "";
                    this._titleElement.textContent = this.questionId ? "Edit Question" : "Create Question";
                }
                break;
        }           
    }

    public questionId: any;
    public question: Question;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".question-name") as HTMLInputElement;}
}

customElements.define(`ce-question-edit-embed`,QuestionEditEmbedComponent);
