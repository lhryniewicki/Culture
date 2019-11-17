import React from 'react';
import '../Shortcuts/Shortcuts.css'

class Shortcuts extends React.Component {

    render() {
        return (
            <div className="card mb-3" >
                <h5 className="card-header text-center">Skróty</h5>
                <ul className="list-group list-group-flush">
                    <div  onClick={this.props.handleClick}>
                        <li name="kalendarz" className="list-group-item liHover "><span className="far fa-calendar-alt mr-2 fa-2x calendarStyle"  ></span> Kalendarz </li>
                    </div>
                    <div  onClick={this.props.handleClick}>
                        <li name="mapa" className="list-group-item liHover "><span className="fas fa-map-marked-alt mr-2 fa-2x mapStyle" ></span>Mapa </li>
                    </div>
                    <div onClick={this.props.handleClick}>
                        <li name="ustawienia" className="list-group-item liHover "><span className="fas fa-cogs mr-2 fa-2x cogStyle" ></span>Ustawienia </li>
                    </div>
                    <div onClick={this.props.handleClick}>
                        <li name="wydarzenia/nowe" className="list-group-item liHover "><span className="fas fa-plus-circle mr-2 fa-2x mapStyle" ></span>Dodaj wydarzenie</li>
                    </div>
                </ul>
            </div>
        );
    }
}
export default Shortcuts;



