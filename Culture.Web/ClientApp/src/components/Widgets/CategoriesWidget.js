import React from 'react';
import '../../components/Widgets/CategoriesWidget.css';

class CategoriesWidget extends React.Component {

    render() {
        return (
            <div className="card my-4">
                <h5 className="card-header">Kategorie</h5>
                <div className="card-body">
                    <div className="row">
                        
                            <div className="col-lg-5 ">
                                <ul className="list-unstyled mb-0">
                                <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Muzyka" type="submit">Muzyka</button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Turystyka" type="submit">Turystyka</button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Styl Życia" type="submit">Styl Życia</button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Regionalia" type="submit">Regionalia</button>
                                </li>
                                <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Wszystkie" type="submit">Wszystkie</button>
                                </li>
                                </ul>
                            </div>
                            <div className="col-lg-7">
                                <ul className="list-unstyled mb-1">
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Nauka i Edukacja" type="submit">Nauka</button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Sport i Rekreacja" type="submit">Sport </button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Dom i Rodzina" type="submit">Dom i Rodzina</button>
                                    </li>
                                    <li>
                                    <button className="myButton" onClick={this.props.handleSearch} name="Kultura i Sztuka" type="submit">Kultura i Sztuka</button>
                                    </li>
                                </ul>
                        </div>
                       
                    </div>
                </div>
            </div>
        );
    }
}
export default CategoriesWidget;



