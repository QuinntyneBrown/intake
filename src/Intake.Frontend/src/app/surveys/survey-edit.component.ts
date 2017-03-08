import { Survey } from "./survey.model";
import { SurveyService } from "./survey.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./survey-edit.component.html");
const styles = require("./survey-edit.component.scss");

export class SurveyEditComponent extends HTMLElement {
    constructor(
		private _surveyService: SurveyService = SurveyService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
		this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["survey-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
	private async _bind() {
        this._titleElement.textContent = this.surveyId ? "Edit Survey": "Create Survey";

        if (this.surveyId) {
            const survey: Survey = await this._surveyService.getById(this.surveyId);                
			this._nameInputElement.value = survey.name;  
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
        const survey = {
            id: this.surveyId,
            name: this._nameInputElement.value
        } as Survey;
        
        await this._surveyService.add(survey);
		this._router.navigate(["survey","list"]);
    }

    public async onDelete() {        
        await this._surveyService.remove({ id: this.surveyId });
		this._router.navigate(["survey","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["survey", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "survey-id":
                this.surveyId = newValue;
				break;
        }        
    }

    public surveyId: number;
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".survey-name") as HTMLInputElement;}
}

customElements.define(`ce-survey-edit`,SurveyEditComponent);
