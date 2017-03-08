import { Question } from "./question.model";
import { QuestionService } from "./question.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./question-edit.component.html");
const styles = require("./question-edit.component.scss");

export class QuestionEditComponent extends HTMLElement {
    constructor(
		private _questionService: QuestionService = QuestionService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["question-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.questionId ? "Edit Question": "Create Question";

        if (this.questionId) {
            const question: Question = await this._questionService.getById(this.questionId);                
			this._nameInputElement.value = question.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
		this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
		this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const question = {
            id: this.questionId,
            name: this._nameInputElement.value
        } as Question;
        
        await this._questionService.add(question);
		this._router.navigate(["question","list"]);
    }

    public async onDelete() {        
        await this._questionService.remove({ id: this.questionId });
		this._router.navigate(["question","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["question", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "question-id":
                this.questionId = newValue;
				break;
        }        
    }

    public questionId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".question-name") as HTMLInputElement;}
}

customElements.define(`ce-question-edit`,QuestionEditComponent);
